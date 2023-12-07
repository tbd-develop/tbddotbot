using System.Collections.Generic;

namespace twitchstreambot.pubsub.Configuration
{
    public class PubSubOptions
    {
        public string Url { get; set; } = null!;
        public IEnumerable<TopicMonitor> Monitor { get; set; } = null!;
        public IDictionary<string, AuthInfo> Users { get; set; } = null!;

        public class TopicMonitor
        {
            public string User { get; set; } = null!;
            public string[] Topics { get; set; } = null!;
        }

        public class AuthInfo
        {
            public string Token { get; set; } = null!;
            public string Refresh { get; set; } = null!;
        }
    }
}