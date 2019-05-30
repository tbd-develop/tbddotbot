using System;
using System.Collections.Generic;

namespace twitchstreambot.Parsing
{
    public class TwitchMessage
    {
        public TwitchUser User { get; set; }
        public TwitchCommand IrcCommand { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public class TwitchUser
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        public static TwitchUser UserFromHeaders(IDictionary<string, string> headers)
        {
            var result = new TwitchUser();

            if (headers.ContainsKey("user-id"))
            {
                int id = 0;

                if (Int32.TryParse(headers["user-id"], out id))
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
}