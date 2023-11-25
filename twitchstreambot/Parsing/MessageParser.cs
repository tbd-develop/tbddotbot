using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace twitchstreambot.Parsing
{
    public abstract class MessageParser
    {
        public abstract TwitchMessage? Do(string input);

        protected Dictionary<string, string> GetHeaders(string input)
        {
            Parser<char> assignment = Parse.Char('=');
            Parser<char> delimiter = Parse.Char(';');
            Parser<char> colon = Parse.Char(':');

            var headersInput = Parse.AnyChar.Until(colon).Text();

            Parser<string> key = Parse.AnyChar.Except(delimiter).Except(assignment).Many().Text();
            Parser<string> value = Parse.AnyChar.Except(delimiter).Many().Text();

            Parser<KeyValuePair<string, string>> keyValue =
                from k in key
                from x in assignment
                from v in value
                select new KeyValuePair<string, string>(k, v);

            var result = keyValue.DelimitedBy(delimiter);

            var headers = result.Parse(headersInput.Parse(input)).ToDictionary(k => k.Key, k => k.Value);

            return headers;
        }
    }
}