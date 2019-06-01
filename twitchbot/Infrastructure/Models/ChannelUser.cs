using System;

namespace twitchbot.Infrastructure.Models
{
    public class ChannelUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int TwitchId { get; set; }
        public string DisplayName { get; set; }
        public int StreamPoints { get; set; }
    }
}