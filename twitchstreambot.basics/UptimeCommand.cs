using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Models;
using twitchstreambot.Parsing;

namespace twitchstreambot.basics
{
    [TwitchCommand("uptime")]
    public class UptimeCommand : ITwitchCommand
    {
        private readonly TwitchConnection _connection;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _clientId;

        public UptimeCommand(TwitchConnection connection,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _connection = connection;
            _httpClientFactory = httpClientFactory;
            _clientId = configuration["twitch:clientId"];
        }

        public bool CanExecute(TwitchMessage message)
        {
            return true;
        }

        public string Execute(TwitchMessage message)
        {
            var statistics = GetStreamStatistics();

            if (statistics is null ||
                statistics.Data.Length == 0)
            {
                return "Stream is currently not active";
            }

            var elapsed = DateTime.UtcNow.Subtract(statistics.Data[0].StartedAt);

            return
                $"Stream has been up for {elapsed.Hours:#0} hours {elapsed.Minutes:#0} minutes";
        }

        private TwitchData? GetStreamStatistics()
        {
            using var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("https://api.twitch.tv/helix/");
            client.DefaultRequestHeaders.Add("Client-Id", _clientId);

            var response = client.GetAsync($"streams/?user_login={_connection.Channel}")
                .Result;

            if (!response.IsSuccessStatusCode) return null;

            var responseData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<TwitchData>(responseData)!;
        }
    }
}