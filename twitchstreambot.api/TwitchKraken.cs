using System.Collections.Generic;
using System.Net.Http;
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

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var channel = JsonConvert.DeserializeObject<TwitchChannel>(content);

                return long.Parse(channel._Id);
            }

            return 0;
        }

        public async Task<IEnumerable<Member>> GetTeamMembers(string name)
        {
            var response = await _client.GetAsync($"kraken/teams/{name}");

            if (response.IsSuccessStatusCode)
            {
                var team =
                    JsonConvert.DeserializeObject<Team>(response.Content
                        .ReadAsStringAsync().GetAwaiter().GetResult());

                return team.Members;
            }

            return null;
        }

        internal class TwitchChannel
        {
            public string _Id { get; set; }
        }

        private class Team
        {
            [JsonProperty("display_name")]
            public string DisplayName { get; set; }
            [JsonProperty("users")]
            public IEnumerable<Member> Members { get; set; }
        }

        public class Member
        {
            public string Name { get; set; }
        }
    }
}