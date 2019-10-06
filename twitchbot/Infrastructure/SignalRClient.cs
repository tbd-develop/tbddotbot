using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace twitchbot.Infrastructure
{
    public class SignalRClient
    {
        private static SignalRClient _instance;
        private HubConnection _hubConnection;

        public delegate void BotReceivedMessageHandler(SignalRClient client, object args);

        public event BotReceivedMessageHandler OnBotMessageReceived;

        private SignalRClient()
        {

        }

        public SignalRClient Initialize(string url)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            return this;
        }

        public async Task<SignalRClient> Start()
        {
            _hubConnection.On("notifychannel", (ChannelNotification notification) =>
                {
                    OnBotMessageReceived?.Invoke(this, notification);
                });

            await _hubConnection.StartAsync();

            await _hubConnection.SendCoreAsync("join", new object[] { "bot" });

            return this;
        }

        public async Task Stop()
        {
            await _hubConnection.SendCoreAsync("leave", new object[] {"bot"});

            await _hubConnection.StopAsync();
        }

        public HubConnection Connection => _hubConnection;

        public static SignalRClient Instance => _instance ?? (_instance = new SignalRClient());
    }
}