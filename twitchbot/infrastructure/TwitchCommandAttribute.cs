using System;

namespace twitchbot.infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TwitchCommandAttribute : Attribute
    {
        public string IdentifyWith { get; }

        public TwitchCommandAttribute(string identifyWith)
        {
            IdentifyWith = identifyWith;
        }
    }
}