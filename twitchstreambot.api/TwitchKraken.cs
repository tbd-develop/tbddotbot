using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace twitchstreambot.api
{
    public class TwitchKraken
    {
        private readonly HttpClient _client;

        public TwitchKraken(HttpClient client)
        {
            _client = client;
        }

        public async Task<long> GetCurrentChannelId()
        {
            var response = await _client.GetAsync($"kraken/channel");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var channel = JsonConvert.DeserializeObject<TwitchChannel>(content);

                return long.Parse(channel._Id);
            }

            return 0;
        }

        public async Task<long> GetChannelIdUsing(string authToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "kraken/channel");

            request.Headers.Add("Authorization", $"OAuth {authToken}");
            request.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var channel = JsonConvert.DeserializeObject<TwitchChannel>(content);

                return long.Parse(channel._Id);
            }

            return 0;
        }

        internal class TwitchChannel
        {
            public string _Id { get; set; }
        }
    }


}