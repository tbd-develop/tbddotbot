using Sprache;

namespace twitchstreambot.Parsing.IRCCommands
{
    public class ParseEnterExit : MessageParser
    {
        private readonly IRCMessageType _command;

        public ParseEnterExit(IRCMessageType command)
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
                User = new TwitchUser { Name = result },
                MessageType = _command,
                Headers = null
            };
        }
    }
}