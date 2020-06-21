using System;
using System.Linq;
using Newtonsoft.Json;

namespace twitchstreambot.pubsub.Models
{
    public class ListenMessage
    {
        public string Type => "LISTEN";
        public string Nonce => 16.RandomString();
        public ListenData Data { get; set; }

        public class ListenData
        {
            public string[] Topics { get; set; }
            [JsonProperty("auth_token")]
            public string AuthToken { get; set; }
        }
    }

    public static class StringExtensions
    {
        private static Random random = new Random();

        public static string RandomString(this int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}