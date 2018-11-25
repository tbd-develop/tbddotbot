using System;
using System.Net.Http;
using Newtonsoft.Json;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot.commands
{
    [TwitchCommand("uptime")]
    public class Uptime : ITwitchCommand
    {
        private readonly TwitchAuthenticator _authenticator;
        private readonly TwitchConnection _connection;

        public Uptime(TwitchAuthenticator authenticator, TwitchConnection connection)
        {
            _authenticator = authenticator;
            _connection = connection;
        }

        public string Execute(params string[] args)
        {
            var statistics = GetStreamStatistics();

            if (statistics?.Stream == null)
            {
                return "Stream is currently not active";
            }

            var elapsed = DateTime.UtcNow.Subtract(statistics.Stream.CreatedAt);

            return
                $"Stream has been up for {elapsed.Hours:#0} hours {elapsed.Minutes:#0} minutes";
        }

        private TwitchData GetStreamStatistics()
        {
            using (HttpClient client = new HttpClient {BaseAddress = new Uri("https://api.twitch.tv/kraken/")})
            {
                var response = client.GetAsync($"streams/{_connection.Channel}?client_id={_authenticator.ClientIdentifier}")
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