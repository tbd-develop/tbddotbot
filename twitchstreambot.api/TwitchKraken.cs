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
        internal class TwitchChannel
        {
            public string _Id { get; set; }
        }
    }

   
}