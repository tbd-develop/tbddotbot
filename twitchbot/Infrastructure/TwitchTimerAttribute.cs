using System;

namespace twitchbot.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TwitchTimerAttribute : Attribute
    {
        public int Seconds { get; }

        public TwitchTimerAttribute(int seconds = 60)
        {
            Seconds = seconds;
        }
    }
}