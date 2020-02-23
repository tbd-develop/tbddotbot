using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<HelixCollectionResponse<UserMarkers>> GetMarkersForUser(string name, string videoId = null)
        {
            var userId = await GetUserIdByName(name);

            if (userId > 0)
            {
                var response = await _client.GetAsync($"helix/streams/markers?user_id={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<HelixCollectionResponse<UserMarkers>>(content);

                    return result;
                }
            }

            return null;
        }
    }

    public class HelixCollectionResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }

    public class StreamMarker
    {
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        public string Description { get; set; }
        [JsonProperty("position_seconds")]
        public string PositionSeconds { get; set; }
        public string Url { get; set; }
    }

    public class UserMarkers
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        public IEnumerable<UserVideo> Videos { get; set; }
    }

    public class UserVideo
    {
        [JsonProperty("video_id")]
        public string VideoId { get; set; }
        public IEnumerable<StreamMarker> Markers { get; set; }
    }

    public class TwitchUser
    {
        public string Id { get; set; }
        public string Login { get; set; }
    }
}