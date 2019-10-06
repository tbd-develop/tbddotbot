using System.Collections.Generic;

namespace twitchbot.Infrastructure
{
    public class ChannelNotification
    {
        public string Message { get; set; }
        public Dictionary<string,int> Data { get; set; }
    }
}