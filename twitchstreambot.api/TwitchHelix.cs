using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace twitchstreambot.api
{
    public class TwitchHelix
    {
        private readonly HttpClient _client;

        public TwitchHelix(HttpClient client)
        {
            _client = client;
        }

        public async Task<long> GetUserIdByName(string name)
        {
            var response = await _client.GetAsync($"helix/users?login={name}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<HelixCollectionResponse<TwitchUser>>(content);

                return long.Parse(users.Data.First().Id);
            }

            return 0;
        }
    }

    public class HelixCollectionResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }


    public class TwitchUser
    {
        public string Id { get; set; }
        public string Login { get; set; }
    }
}