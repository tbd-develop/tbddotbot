using System;
using Sprache;

namespace twitchstreambot.Parsing
{
    public class TwitchCommandParser
    {
        private static readonly Parser<TwitchCommand> CommandParser = Parse.String("PRIVMSG").Select(_ => TwitchCommand.PRIVMSG)
            .Or(Parse.String("JOIN").Select(_ => TwitchCommand.JOIN))
            .Or(Parse.String("PART").Select(_ => TwitchCommand.PART))
            .Or(Parse.String("USERNOTICE").Select(_ => TwitchCommand.USERNOTICE))
            .Or(Parse.String("USERSTATE").Select(_ => TwitchCommand.USERSTATE));

        private static readonly Func<Parser<TwitchCommand>, Parser<TwitchCommand>> LineParser = p => from x in Parse.AnyChar.Until(Parse.String(".twitch.tv"))
                                                                                                                     from l in Parse.WhiteSpace
                                                                                                                     from c in p
                                                                                                                     from lr in Parse.WhiteSpace
                                                                                                                     from r in Parse.AnyChar.Many()
                                                                                                                     select c;


        public static Func<string, bool> IsMatch = input =>
            LineParser(CommandParser).TryParse(input).WasSuccessful;

        public static TwitchMessage Gather(string input)
        {
            var command = LineParser(CommandParser).Parse(input);

            switch (command)
            {
                case TwitchCommand.USERNOTICE:
                    {
                        return new ParseNoticeMessage().Do(input);
                    }
                case TwitchCommand.PRIVMSG:
                    {
                        return new ParsePrivMessage().Do(input);
                    }
                case TwitchCommand.USERSTATE:
                    {
                        return new ParseUserState().Do(input);
                    }
                case TwitchCommand.PART:
                case TwitchCommand.JOIN:
                    {
                        return new ParseEnterExit(command).Do(input);
                    }
            }

            return null;
        }
    }
}