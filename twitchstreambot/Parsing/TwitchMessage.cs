using System;
using System.Collections.Generic;

namespace twitchstreambot.Parsing
{
    public class TwitchMessage
    {
        public TwitchUser User { get; set; }
        public IRCMessageType MessageType { get; set; }
        public BotCommand Command { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Content { get; set; }

        public bool IsBotCommand => Command != null;

        public static TwitchUser UserFromHeaders(IDictionary<string, string> headers)
        {
            var result = new TwitchUser();

            if (headers.ContainsKey("user-id"))
            {
                if (int.TryParse(headers["user-id"], out int id))
                {
                    result.Id = id;
                }
            }

            if (headers.ContainsKey("display-name"))
            {
                result.Name = headers["display-name"];
            }

            return result;
        }
    }

    public class TwitchUser
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class BotCommand
    {
        public string Action { get; set; }
        public IEnumerable<string> Arguments { get; set; }
    }
}