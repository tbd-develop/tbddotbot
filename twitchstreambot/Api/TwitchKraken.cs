using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using twitchstreambot.Models;

namespace twitchstreambot.Api
{
    public class TwitchKraken
    {
        private readonly HttpClient _client;

        public TwitchKraken(HttpClient client)
        {
            _client = client;
        }

        public async Task<ChannelResponse> GetChannel(string clientId, string authToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}kraken/channel");

            request.Headers.Add("Client-Id", clientId);
            request.Headers.Add("Authorization", $"OAuth {authToken}");
            request.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChannelResponse>(await response.Content.ReadAsStringAsync());

                return result;
            }

            return default;
        }

        public async Task<IEnumerable<TwitchKrakenUser>> GetUsers(params string[] names)
        {
            var response = await _client.GetAsync($"kraken/users?login={string.Join(',', names)}");

            if (response.IsSuccessStatusCode)
            {
                var result =
                    JsonConvert.DeserializeObject<TwitchUsersSet<TwitchKrakenUser>>(response.Content
                        .ReadAsStringAsync().GetAwaiter().GetResult());

                return result.Users;
            }

            return null;
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

        internal class TwitchUsersSet<T>
        {
            [JsonProperty("_total")]
            public int Total { get; set; }
            public IEnumerable<T> Users { get; set; }
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