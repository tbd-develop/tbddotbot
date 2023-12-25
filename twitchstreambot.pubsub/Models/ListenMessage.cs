using System.Text.Json.Serialization;

namespace twitchstreambot.pubsub.Models;

public class ListenMessage
{
    public string Type => "LISTEN";
    public string Nonce => 16.RandomString();
    public ListenData Data { get; set; }

    public class ListenData
    {
        public string[] Topics { get; set; }
        [JsonPropertyName("auth_token")]
        public string AuthToken { get; set; }
    }
}