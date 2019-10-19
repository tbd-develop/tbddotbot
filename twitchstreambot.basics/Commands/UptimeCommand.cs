using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using twitchstreambot.infrastructure;
using twitchstreambot.models;
using twitchstreambot.Parsing;

namespace twitchbot.commands
{
    [TwitchCommand("uptime")]
    public class UptimeCommand : ITwitchCommand
    {
        private readonly TwitchConnection _connection;
        private readonly string _clientId;

        public UptimeCommand(TwitchConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _clientId = configuration["twitch:clientId"];
        }

        public bool CanExecute(TwitchMessage message)
        {
            return true;
        }

        public string Execute(TwitchMessage message)
        {
            var statistics = GetStreamStatistics();

            if (statistics?.Data.Length == 0)
            {
                return "Stream is currently not active";
            }

            var elapsed = DateTime.UtcNow.Subtract(statistics.Data[0].StartedAt);

            return
                $"Stream has been up for {elapsed.Hours:#0} hours {elapsed.Minutes:#0} minutes";
        }

        private TwitchData GetStreamStatistics()
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri("https://api.twitch.tv/helix/") })
            {
                client.DefaultRequestHeaders.Add("Client-Id", _clientId);

                var response = client.GetAsync($"streams/?user_login={_connection.Channel}")
                    .Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<TwitchData>(responseData);
                }
            }

            return null;
        }
    }
}