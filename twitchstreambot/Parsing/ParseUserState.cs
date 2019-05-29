using System;

namespace twitchstreambot.Parsing
{
    public class ParseUserState : MessageParser
    {
        public override TwitchMessage Do(string input)
        {
            Console.WriteLine("Here");

            var headers = GetHeaders(input);

            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                IRCCommand = "USERSTATE",
                Message = "",
                Headers = headers
            };
        }
    }
}