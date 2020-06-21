using Newtonsoft.Json;

namespace twitchstreambot.pubsub.Messages
{
    public class TwitchUserResponse
    {
        public long Id { get; set; }
        public string Login { get; set; }
        [JsonProperty("display_name")] public string DisplayName { get; set; }
    }
}