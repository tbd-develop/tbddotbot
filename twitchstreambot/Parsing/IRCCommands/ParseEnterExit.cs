using Sprache;

namespace twitchstreambot.Parsing
{
    public class ParseEnterExit : MessageParser
    {
        private readonly TwitchCommand _command;

        public ParseEnterExit(TwitchCommand command)
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
                IrcCommand = _command,
                Headers = null
            };
        }
    }
}