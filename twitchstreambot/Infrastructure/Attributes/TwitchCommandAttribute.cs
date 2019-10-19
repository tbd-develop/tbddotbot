using System;

namespace twitchstreambot.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TwitchCommandAttribute : Attribute
    {
        public string Action { get; }
        public bool Ignore { get; set; }
        public bool IsPrivate { get; set; } 

        public TwitchCommandAttribute(string actionName)
        {
            Action = actionName;
        }
    }
}