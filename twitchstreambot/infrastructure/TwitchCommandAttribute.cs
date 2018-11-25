using System;

namespace twitchstreambot.infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TwitchCommandAttribute : Attribute
    {
        public string IdentifyWith { get; }
        public bool Ignore { get; set; }

        public TwitchCommandAttribute(string identifyWith)
        {
            IdentifyWith = identifyWith;
        }
    }
}