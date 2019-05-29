using System;
using System.Linq;
using Sprache;

namespace twitchstreambot.Parsing
{
    public class ParsePrivMessage : MessageParser
    {
        private Parser<char> _colon = Parse.Char(':');

        public override TwitchMessage Do(string input)
        {
            var headers = GetHeaders(input);

            var results = Parse.AnyChar.Except(_colon).Many().Text();
            var extractRestOfString = results.DelimitedBy(_colon).Select(s => s.Where(x => !String.IsNullOrWhiteSpace(x)));

            var stringSplit = Parse.AnyChar.Except(Parse.WhiteSpace).Many().Text();

            var stringSplitter = stringSplit.DelimitedBy(Parse.WhiteSpace);

            var elements = extractRestOfString.Parse(input);

            var message = elements.ElementAt(2);
            var command = stringSplitter.Parse(elements.ElementAt(1)).ElementAt(1);

            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                IRCCommand = command,
                Message = message,
                Headers = headers
            };
        }
    }
}