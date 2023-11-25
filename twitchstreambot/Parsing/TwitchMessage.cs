using System;
using System.Collections.Generic;

namespace twitchstreambot.Parsing
{
    public class TwitchMessage
    {
        public TwitchUser? User { get; set; }
        public IRCMessageType MessageType { get; set; }
        public BotCommand? Command { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public string? Content { get; set; }

        public bool IsBotCommand => Command != null;

        public static TwitchUser UserFromHeaders(IDictionary<string, string> headers)
        {
            var result = new TwitchUser();

            if (headers.TryGetValue("user-id", out var userIdHeader))
            {
                if (int.TryParse(userIdHeader, out var id))
                {
                    result.Id = id;
                }
            }

            if (headers.TryGetValue("display-name", out var displayNameHeader))
            {
                result.Name = displayNameHeader;
            }

            return result;
        }
    }
}