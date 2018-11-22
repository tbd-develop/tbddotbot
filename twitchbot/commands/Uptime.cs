using System;
using System.Net.Http;
using Newtonsoft.Json;
using twitchbot.infrastructure;
using twitchbot.models;

namespace twitchbot.commands
{
    public class Uptime : ITwitchCommand
    {
        private readonly Auth _authentication;
        private readonly TwitchConnection _connection;

        public Uptime(Auth authentication, TwitchConnection connection)
        {
            _authentication = authentication;
            _connection = connection;
        }

        public string Execute(params string[] args)
        {
            var statistics = GetStreamStatistics();

            var elapsed = DateTime.UtcNow.Subtract(statistics.Stream.CreatedAt);

            return
                $"Stream has been up for {elapsed.Hours:#0} hours {elapsed.Minutes:#0} minutes";
        }

        private TwitchData GetStreamStatistics()
        {
            using (HttpClient client = new HttpClient {BaseAddress = new Uri("https://api.twitch.tv/kraken/")})
            {
                var response = client.GetAsync($"streams/{_connection.Channel}?client_id={_authentication.ClientId}").Result;

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