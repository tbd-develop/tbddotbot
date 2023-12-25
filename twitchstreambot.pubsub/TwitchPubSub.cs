using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using twitchstreambot.Api;
using twitchstreambot.pubsub.Configuration;
using twitchstreambot.pubsub.Infrastructure;
using twitchstreambot.pubsub.Messaging;
using twitchstreambot.pubsub.Models;

namespace twitchstreambot.pubsub;

public class TwitchPubSub
{
    private readonly TwitchApi _api;
    private readonly TwitchHelix _helix;
    public const int BufferSize = 1024;

    private readonly PubSubOptions _configuration;
    private readonly ClientWebSocket _webSocketClient;
    private bool _exiting;
    private readonly IDictionary<string, Dictionary<string, object>> _userData;
    private Task _listenTask = null!;
    private Task _pingTask;

    public delegate void PubSubConnectedDelegate(CancellationToken token);

    public delegate void SubscriptionErrorDelegate(string message);

    public event SubscriptionErrorDelegate OnSubscriptionError;
    public event PubSubConnectedDelegate OnPubSubConnected;

    public TwitchPubSub(
        IConfiguration configuration,
        TwitchApi api,
        TwitchHelix helix)
    {
        _api = api;
        _helix = helix;
        _configuration = new PubSubOptions();
        _userData = new Dictionary<string, Dictionary<string, object>>();

        configuration.GetSection("twitch:pubsub")
            .Bind(_configuration);

        _webSocketClient = new ClientWebSocket();
    }

    private async void RunTimedPing()
    {
        while (!_exiting)
        {
            await PingServer(CancellationToken.None);

            Thread.Sleep(1000 * 60 * RandomNumberGenerator.GetInt32(3, 6));
        }
    }

    public async Task Stop(CancellationToken token)
    {
        _exiting = true;

        Task.WaitAll(new[]
            {
                _listenTask
            }, cancellationToken:
            token);

        await _webSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutting down pub sub", token);
    }

    public async Task Connect(CancellationToken cancellationToken)
    {
        await _webSocketClient.ConnectAsync(new Uri(_configuration.Url), cancellationToken);

        if (_webSocketClient.State == WebSocketState.Open)
        {
            await SubscribeToTopics(cancellationToken);

            var responseObject = await GetResult<PubSubListenResponse>(cancellationToken);

            if (responseObject is not null && responseObject.IsError)
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

    public async Task Listen(CancellationToken cancellationToken)
    {
        var bufferContent = new byte[BufferSize];
        var buffer = new ArraySegment<byte>(bufferContent);
        StringBuilder message = new StringBuilder();

        _pingTask = new Task(RunTimedPing, cancellationToken, TaskCreationOptions.AttachedToParent);

        _pingTask.Start();

        do
        {
            var response = await _webSocketClient.ReceiveAsync(buffer, cancellationToken);

            if (buffer.Array is not null)
                message.Append(Encoding.UTF8.GetString(buffer.Array, 0, response.Count));

            if (!response.EndOfMessage) continue;

            var content = JObject.Parse(message.ToString());

            //await DispatchEvent(message.ToString());

            //var response = await GetResult<PubSubMessageBase>(cancellationToken);

            //if (response == null || response.Type != "PONG")
            //{
            //    await Connect(cancellationToken);
            //}

            message.Clear();

            await Task.Delay(50, cancellationToken);
        } while (!_exiting || !cancellationToken.IsCancellationRequested);
    }

    private async Task<T?> GetResult<T>(CancellationToken cancellationToken)
    {
        var bufferContent = new byte[BufferSize];
        var buffer = new ArraySegment<byte>(bufferContent);

        var response = await _webSocketClient.ReceiveAsync(buffer, cancellationToken);

        if (buffer.Array is null) return default;

        var responseContent = Encoding.UTF8.GetString(buffer.Array, 0, response.Count);

        var responseObject = JsonConvert.DeserializeObject<T>(responseContent);

        return responseObject;
    }

    private async Task SubscribeToTopics(CancellationToken cancellationToken)
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
        return await Task.FromResult(new ListenMessage());
        // if (!_userData.TryGetValue(monitor.User, out var value))
        // {
        //     var verification = await _api.Validate(_configuration.Users[monitor.User].Token);
        //
        //     if (verification == null)
        //     {
        //         throw new InvalidCredentialException($"User {monitor.User} is not verified");
        //     }
        //
        //     var response = await _helix.GetChannels(monitor.User);
        //
        //     if (response is null || !response.HasData)
        //     {
        //         return default;
        //     }
        //
        //     value = new Dictionary<string, object>
        //     {
        //         { "userId", verification.UserId },
        //         { "channelId", response. }
        //     };
        //     _userData.Add(monitor.User, value);
        // }
        //
        // var message = new ListenMessage()
        // {
        //     Data = new ListenMessage.ListenData
        //     {
        //         AuthToken = _configuration.Users[monitor.User].Token,
        //         Topics = monitor.Topics.Select(topic =>
        //             PubSubTopic.ByName(topic).For(value)).ToArray()
        //     }
        // };
        //
        // return message;
    }
}