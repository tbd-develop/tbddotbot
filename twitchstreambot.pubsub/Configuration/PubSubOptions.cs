using System.Collections.Generic;

namespace twitchstreambot.pubsub.Configuration
{
    public class PubSubOptions
    {
        public string Url { get; set; }
        public IEnumerable<TopicMonitor> Monitor { get; set; }
        public IDictionary<string, string> Users { get; set; }

        public class TopicMonitor
        {
            public string User { get; set; }
            public string[] Topics { get; set; }
        }
    }
}