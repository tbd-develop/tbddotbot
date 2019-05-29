using System;
using Sprache;

namespace twitchstreambot.Parsing
{
    public class TwitchCommandParser
    {
        private static Parser<TwitchCommands> _commandParser = Parse.String("PRIVMSG").Select(_ => TwitchCommands.PRIVMSG)
            .Or(Parse.String("JOIN").Select(_ => TwitchCommands.JOIN))
            .Or(Parse.String("PART").Select(_ => TwitchCommands.PART))
            .Or(Parse.String("USERNOTICE").Select(_ => TwitchCommands.USERNOTICE))
            .Or(Parse.String("USERSTATE").Select(_ => TwitchCommands.USERSTATE));

        private static Func<Parser<TwitchCommands>, Parser<TwitchCommands>> _lineParser = (Parser<TwitchCommands> p) => from x in Parse.AnyChar.Until(Parse.String(".twitch.tv"))
            from l in Parse.WhiteSpace
            from c in p
            from lr in Parse.WhiteSpace
            from r in Parse.AnyChar.Many()
            select c;


        public static Func<string, bool> IsMatch = (string input) =>
        {
            return _lineParser(_commandParser).TryParse(input).WasSuccessful;
        };

        public static TwitchMessage Gather(string input)
        {
            var command = _lineParser(_commandParser).Parse(input);

            switch (command)
            {
                case TwitchCommands.USERNOTICE:
                {
                    return new ParseNoticeMessage().Do(input);
                }
                case TwitchCommands.PRIVMSG:
                {
                    return new ParsePrivMessage().Do(input);
                }
                case TwitchCommands.USERSTATE:
                {
                    return new ParseUserState().Do(input);
                }
                case TwitchCommands.PART:
                case TwitchCommands.JOIN:
                {
                    return new ParseEnterExit(command.ToString()).Do(input);
                }
            }

            return null;
        }
    }
}