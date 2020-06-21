using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using twitchstreambot.api;
using twitchstreambot.pubsub.Configuration;
using twitchstreambot.pubsub.Infrastructure;
using twitchstreambot.pubsub.Messaging;
using twitchstreambot.pubsub.Models;

namespace twitchstreambot.pubsub
{
    public class TwitchPubSub
    {
        private readonly TwitchApi _api;
        private readonly TwitchKraken _kraken;
        public const int BufferSize = 1024;

        private readonly PubSubOptions _configuration;
        private readonly ClientWebSocket _webSocketClient;
        private bool _exiting;
        private readonly IDictionary<string, Dictionary<string, object>> _userData;
        private Task _listenTask;

        public delegate void PubSubConnectedDelegate(CancellationToken token);
        public delegate void SubscriptionErrorDelegate(string message);

        public event SubscriptionErrorDelegate OnSubscriptionError;
        public event PubSubConnectedDelegate OnPubSubConnected;

        public TwitchPubSub(IConfiguration configuration, TwitchApi api, TwitchKraken kraken)
        {
            _api = api;
            _kraken = kraken;
            _configuration = new PubSubOptions();
            _userData = new Dictionary<string, Dictionary<string, object>>();

            configuration.GetSection("twitch:pubsub").Bind(_configuration);

            _webSocketClient = new ClientWebSocket();
        }

        public async Task Stop(CancellationToken token)
        {
            _exiting = true;

            Task.WaitAll(_listenTask);

            await _webSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutting down pub sub", token);
        }

        public async Task Connect(CancellationToken cancellationToken)
        {
            await _webSocketClient.ConnectAsync(new Uri(_configuration.Url), cancellationToken);

            if (_webSocketClient.State == WebSocketState.Open)
            {
                await SubscribeToTopics(cancellationToken);

                var responseObject = await GetResult<PubSubListenResponse>(cancellationToken);

                if (responseObject.IsError)
                {
                    await _webSocketClient.CloseAsync(WebSocketCloseStatus.InternalServerError, "Not Allowed",
                        CancellationToken.None);

                    OnSubscriptionError?.Invoke("Unable to subscribe to topics");
                }

                OnPubSubConnected?.Invoke(cancellationToken);
            }
            else
            {
                throw new Exception("Unable to connect to Twitch PubSub");
            }
        }

        public void Listen(CancellationToken cancellationToken)
        {
            _listenTask = new Task(async () =>
            {
                byte[] bufferContent = new byte[BufferSize];
                var buffer = new ArraySegment<byte>(bufferContent);
                StringBuilder message = new StringBuilder();

                do
                {
                    var response = await _webSocketClient.ReceiveAsync(buffer, cancellationToken);

                    message.Append(Encoding.UTF8.GetString(buffer.Array, 0, response.Count));

                    if (response.EndOfMessage)
                    {

                        var content = JObject.Parse(message.ToString());

                        //await DispatchEvent(message.ToString());

                        //var response = await GetResult<PubSubMessageBase>(cancellationToken);

                        //if (response == null || response.Type != "PONG")
                        //{
                        //    await Connect(cancellationToken);
                        //}

                        message.Clear();

                        await Task.Delay(50, cancellationToken);
                    }
                } while (!_exiting);
            });

            _listenTask.Start();
        }

        private async Task<T> GetResult<T>(CancellationToken cancellationToken)
        {
            byte[] bufferContent = new byte[BufferSize];
            var buffer = new ArraySegment<byte>(bufferContent);

            var response = await _webSocketClient.ReceiveAsync(buffer, cancellationToken);

            var responseContent = Encoding.UTF8.GetString(buffer.Array, 0, response.Count);

            var responseObject = JsonConvert.DeserializeObject<T>(responseContent);

            return responseObject;
        }

        public async Task SubscribeToTopics(CancellationToken cancellationToken)
        {
            foreach (var listen in _configuration.Monitor)
            {
                var message = await ConstructListenMessage(listen);

                await SendMessage(message, cancellationToken);
            }
        }

        private async Task PingServer(CancellationToken cancellationToken)
        {
            await SendMessage(new { Type = "PING" }, cancellationToken);
        }

        private async Task SendMessage<T>(T message, CancellationToken token)
        {
            await _webSocketClient.SendAsync(message.AsJsonBytesArraySegment(), WebSocketMessageType.Text, true, token);
        }

        private async Task<ListenMessage> ConstructListenMessage(PubSubOptions.TopicMonitor monitor)
        {
            if (!_userData.ContainsKey(monitor.User))
            {
                var verification = await _api.Validate(_configuration.Users[monitor.User].Token);

                if (verification == null)
                {
                    throw new InvalidCredentialException($"User {monitor.User} is not verified");
                }

                var channel = await _kraken.GetChannel(verification.ClientId, _configuration.Users[monitor.User].Token);

                _userData.Add(monitor.User, new Dictionary<string, object>
                {
                    {"userId", verification.UserId},
                    {"channelId", channel.Id}
                });
            }

            List<string> topicsToSubscribeTo = new List<string>();

            foreach (var topic in monitor.Topics)
            {
                topicsToSubscribeTo.Add(PubSubTopic.ByName(topic).For(_userData[monitor.User]));
            }

            var message = new ListenMessage()
            {
                Data = new ListenMessage.ListenData
                {
                    AuthToken = _configuration.Users[monitor.User].Token,
                    Topics = topicsToSubscribeTo.ToArray()
                }
            };

            return message;
        }
    }
}