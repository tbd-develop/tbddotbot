using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.Configuration
{
    public class TwitchBotConfiguration
    {
        public string AuthToken { get; set; }
        public string ClientId { get; set; }
        public string Refresh { get; set; }
    }
}