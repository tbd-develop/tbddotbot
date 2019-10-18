using System;
using System.Collections.Generic;
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

            var botCommand = GetCommand(elements.ElementAt(2));

            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                IrcCommand = TwitchCommand.PRIVMSG,
                Command = botCommand,
                Headers = headers
            };
        }

        private BotCommand GetCommand(string input)
        {
            if (_exclamation.TryParse(input).WasSuccessful)
            {
                Parser<string> _wordParser =
                    Parse.LetterOrDigit.Or(Parse.Chars('[', ']', '#', '!', ',', '.', '@', '!', '(', ')')).Many().Text()
                        .Token();

                var arguments =
                    Parse.Repeat(from leading in _wordParser
                                 from rest in Parse.WhiteSpace.Many()
                                 select leading, 75);

                var parser = from leading in _exclamation
                             from first in _wordParser
                             from after in Parse.AnyChar.Many().Text()
                             select new { Command = first, Arguments = arguments.Parse(after) };

                var result = parser.Parse(input);

                if (result.Command != null)
                {
                    return new BotCommand { Action = result.Command, Arguments = result.Arguments };
                }
            }

            return null;
        }
    }
}