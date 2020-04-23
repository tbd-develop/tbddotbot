using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using twitchstreambot.api.Infrastructure;
using twitchstreambot.api.Models;

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

        public async Task CreateStreamMarkerForUser(string name, string comment)
        {
            var userId = await GetUserIdByName(name);

            if (userId > 0)
            {
                var message = JsonConvert.SerializeObject(new { user_id = userId, description = comment });

                await _client.PostAsync($"helix/streams/markers",
                    new StringContent(message, Encoding.UTF8, "application/json"));
            }
        }

        public async Task<HelixCollectionResponse<StreamMarkerData>> GetMarkersForUser(string name)
        {
            var userId = await GetUserIdByName(name);

            if (userId > 0)
            {
                var response = await _client.GetAsync($"helix/streams/markers?user_id={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<HelixCollectionResponse<StreamMarkerData>>(content);

                    return result;
                }
            }

            return null;
        }

        public async Task<HelixCollectionResponse<StreamMarkerData>> GetMarkersForVideo(string id)
        {
            var response = await _client.GetAsync($"helix/streams/markers?video_id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<HelixCollectionResponse<StreamMarkerData>>(content);

                return result;
            }

            return null;
        }

        public async Task<HelixCollectionResponse<TwitchVideo>> GetVideosForUser(string name, string type = "archive")
        {
            var userId = await GetUserIdByName(name);

            if (userId > 0)
            {
                var response = await _client.GetAsync($"helix/videos?user_id={userId}&type={type}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<HelixCollectionResponse<TwitchVideo>>(content);

                    return result;
                }
            }

            return null;
        }
    }
}