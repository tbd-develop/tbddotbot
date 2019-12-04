﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using twitchstreambot.api;
using twitchstreambot.pubsub.Configuration;
using twitchstreambot.pubsub.Messaging;
using twitchstreambot.pubsub.Models;

namespace twitchstreambot.pubsub
{
    public class TwitchPubSub
    {
        private readonly TwitchHelix _helix;
        private readonly TwitchKraken _kraken;
        public const int BufferSize = 1024;

        private readonly PubSubOptions _configuration;
        private readonly ClientWebSocket _webSocketClient;
        private readonly string _auth;
        private bool _exiting;
        private readonly IDictionary<string, Dictionary<string, long>> _userCache;
        private readonly MessageProcessor _processor;

        public delegate void WhisperReceivedDelegate(PubSubResponseMessage<PubSubWhisperResponse> responseMessage);

        public delegate void SubscriptionErrorDelegate(string message);

        public event WhisperReceivedDelegate OnWhisperReceived;
        public event SubscriptionErrorDelegate OnSubscriptionError;

        public TwitchPubSub(IConfiguration configuration, TwitchHelix helix, TwitchKraken kraken)
        {
            _helix = helix;
            _kraken = kraken;
            _configuration = new PubSubOptions();
            _auth = configuration["twitch:auth"];
            _userCache = new Dictionary<string, Dictionary<string, long>>();

            configuration.GetSection("twitch:pubsub").Bind(_configuration);

            _webSocketClient = new ClientWebSocket();

            _processor = new MessageProcessor(this);
            _processor.On("whispers", (content, publisher) =>
            {
                var thingToPublish =
                    JsonConvert.DeserializeObject<PubSubResponseMessage<PubSubWhisperResponse>>(content);

                publisher.OnWhisperReceived(thingToPublish);
            });
        }

        public async Task Connect(CancellationToken cancellationToken)
        {
            await _webSocketClient.ConnectAsync(new Uri(_configuration.Url), cancellationToken);

            if (_webSocketClient.State == WebSocketState.Open)
            {
                await StartListening(cancellationToken);

                byte[] bufferContent = new byte[BufferSize];
                var buffer = new ArraySegment<byte>(bufferContent);

                var response = await _webSocketClient.ReceiveAsync(buffer, cancellationToken);

                var responseContent = Encoding.UTF8.GetString(buffer.Array, 0, response.Count);

                var responseObject = JsonConvert.DeserializeObject<PubSubListenResponse>(responseContent);

                if (responseObject.IsError)
                {
                    await _webSocketClient.CloseAsync(WebSocketCloseStatus.InternalServerError, "Not Allowed",
                        CancellationToken.None);

                    OnSubscriptionError?.Invoke("Unable to subscribe to topics");
                }
            }
            else
            {
                throw new Exception("Unable to connect to Twitch PubSub");
            }
        }

        private async Task StartListening(CancellationToken cancellationToken)
        {
            foreach (var listen in _configuration.Monitor)
            {
                var message = await ConstructListenMessage(listen);

                await _webSocketClient.SendAsync(
                    new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))),
                    WebSocketMessageType.Text, true, cancellationToken);
            }
        }

        private async Task<ListenMessage> ConstructListenMessage(PubSubOptions.TopicMonitor monitor)
        {
            if (!_userCache.ContainsKey(monitor.User))
            {
                var subscriptionUserId = await _helix.GetUserIdByName(monitor.User);
                var subscriptionChannelId = await _kraken.GetChannelIdUsing(_configuration.Users[monitor.User]);

                _userCache.Add(monitor.User, new Dictionary<string, long>
                {
                    {"userId", subscriptionUserId},
                    {"channelId", subscriptionChannelId}
                });
            }

            List<string> topicsToSubscribeTo = new List<string>();

            foreach (var topic in monitor.Topics)
            {
                topicsToSubscribeTo.Add(PubSubTopic.ByName(topic).For(_userCache[monitor.User]));
            }

            var message = new ListenMessage()
            {
                Data = new ListenMessage.ListenData
                {
                    AuthToken = _configuration.Users[monitor.User],
                    Topics = topicsToSubscribeTo.ToArray()
                }
            };

            return message;
        }

        public Task Listen(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                await Connect(cancellationToken);

                byte[] bufferContent = new byte[BufferSize];
                var buffer = new ArraySegment<byte>(bufferContent);
                StringBuilder message = new StringBuilder();

                do
                {
                    var response = await _webSocketClient.ReceiveAsync(buffer, cancellationToken);

                    message.Append(Encoding.UTF8.GetString(buffer.Array, 0, response.Count));

                    if (response.EndOfMessage)
                    {
                        DispatchEvent(message.ToString());

                        message = new StringBuilder();

                        await Task.Delay(50, cancellationToken);
                    }
                } while (!_exiting);
            }, cancellationToken);
        }

        public async Task Stop(CancellationToken token)
        {
            _exiting = true;

            await _webSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutting down bot", token);
        }

        private void DispatchEvent(string message)
        {
            var response = JsonConvert.DeserializeObject<PubSubResponseMessage>(message);

            if (response.Type == "PING")
            {
                //SendMessage("PONG");
            }

            if (response.Type == "RECONNECT")
            {
                // Reconnect Client
            }

            if (response.Type == "MESSAGE")
            {
                File.WriteAllText(@"c:\temp\pubsub_sample.json", message);

                var topicWasReceived = response.Data.Topic.Split('.')[0];

                _processor.Process(topicWasReceived, message);
            }
        }
    }

    public class MessageProcessor
    {
        private readonly TwitchPubSub _client;
        private readonly Dictionary<string, Action<string, TwitchPubSub>> _actions;

        public MessageProcessor(TwitchPubSub client)
        {
            _client = client;
            _actions = new Dictionary<string, Action<string, TwitchPubSub>>();
        }

        public MessageProcessor On(string message, Action<string, TwitchPubSub> action)
        {
            _actions.Add(message, action);

            return this;
        }

        public void Process(string action, string content)
        {
            if (_actions.ContainsKey(action))
            {
                _actions[action](content, _client);
            }
        }
    }
}