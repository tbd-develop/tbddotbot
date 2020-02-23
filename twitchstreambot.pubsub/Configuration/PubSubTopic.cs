using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace twitchstreambot.pubsub.Configuration
{
    public class PubSubTopic
    {
        public static PubSubTopic Whisper = new PubSubTopic("whispers", "whispers", "userId");
        public static PubSubTopic Bits = new PubSubTopic("bits", "channel-bits-events-v2", "channelId");
        public static PubSubTopic Subscriptions =
            new PubSubTopic("subscriptions", "channel-subscribe-events-v1", "channelId");

        private readonly string _name;
        private readonly string _topicIdentifier;
        private readonly string _identifier;

        private static Lazy<Dictionary<string, PubSubTopic>> TopicsByName =>
            new Lazy<Dictionary<string, PubSubTopic>>(() =>
                (from t in typeof(PubSubTopic).GetFields(BindingFlags.Public | BindingFlags.Static)
                 where t.FieldType == typeof(PubSubTopic)
                 let fieldTopic = (PubSubTopic)t.GetValue(null)
                 let fieldName = fieldTopic._name
                 select new { Name = fieldName, Topic = fieldTopic }).ToDictionary(k => k.Name, k => k.Topic));

        private PubSubTopic(string name, string topicIdentifier, string identifier)
        {
            _name = name;
            _topicIdentifier = topicIdentifier;
            _identifier = identifier;
        }

        public static PubSubTopic ByName(string name)
        {
            if (!TopicsByName.Value.ContainsKey(name))
            {
                return null;
            }

            return TopicsByName.Value[name];
        }

        public string For(IDictionary<string, long> identifiers)
        {
            return $"{_topicIdentifier}.{identifiers[_identifier]}";
        }
    }
}