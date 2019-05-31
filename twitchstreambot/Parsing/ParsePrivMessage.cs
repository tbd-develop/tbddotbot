using System;
using System.Linq;
using Sprache;

namespace twitchstreambot.Parsing
{
    public class ParsePrivMessage : MessageParser
    {
        private readonly Parser<char> _colon = Parse.Char(':');
        private readonly Parser<char> _exclamation = Parse.Char('!');

        public override TwitchMessage Do(string input)
        {
            var headers = GetHeaders(input);

            var results = Parse.AnyChar.Except(_colon).Many().Text();

            var extractRestOfString = results.DelimitedBy(_colon).Select(s => s.Where(x => !string.IsNullOrWhiteSpace(x)));

            var elements = extractRestOfString.Parse(input);

            var message = elements.ElementAt(2);

            var command = GetCommand(message);
            
            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                IrcCommand = TwitchCommand.PRIVMSG,
                IsBotCommand = !string.IsNullOrEmpty(command),
                BotCommand = command,
                Message = message,
                Headers = headers
            };
        }

        private string GetCommand(string input)
        {
            if (_exclamation.TryParse(input).WasSuccessful)
            {
                var cp = from x in _exclamation
                         from l in Parse.AnyChar.Many().Text()
                    select l;

                return cp.Parse(input);
            }

            return null;
        }
    }
}