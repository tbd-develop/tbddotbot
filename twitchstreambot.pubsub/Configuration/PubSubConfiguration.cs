using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace twitchstreambot.pubsub.Configuration
{
    public class PubSubConfiguration
    {
        public IReadOnlyList<string> Topics => new ReadOnlyCollection<string>(_topics);

        private IList<string> _topics;

        public PubSubConfiguration SubscribeTopic(string topic)
        {
            if (_topics == null)
            {
                _topics = new List<string>();
            }

            _topics.Add(topic);

            return this;
        }
    }
}