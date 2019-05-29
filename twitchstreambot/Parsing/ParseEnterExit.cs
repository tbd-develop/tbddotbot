using Sprache;

namespace twitchstreambot.Parsing
{
    public class ParseEnterExit : MessageParser
    {
        private readonly string _command;

        public ParseEnterExit(string command)
        {
            _command = command;
        }

        public override TwitchMessage Do(string input)
        {
            var usernameParser = from skip in Parse.Char(':')
                from u in Parse.AnyChar.Until(Parse.Char('!')).Text()
                select u;

            var result = usernameParser.Parse(input);

            return new TwitchMessage
            {
                User = new TwitchMessage.TwitchUser { Name = result },
                Message = string.Empty,
                IRCCommand = _command,
                Headers = null
            };
        }
    }
}