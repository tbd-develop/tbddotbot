using System;
using Sprache;
using twitchstreambot.Parsing.IRCCommands;

namespace twitchstreambot.Parsing
{
    public class TwitchCommandParser
    {
        private static readonly Parser<IRCMessageType> CommandParser = Parse.String("PRIVMSG").Select(_ => IRCMessageType.PRIVMSG)
            .Or(Parse.String("JOIN").Select(_ => IRCMessageType.JOIN))
            .Or(Parse.String("PART").Select(_ => IRCMessageType.PART))
            .Or(Parse.String("USERNOTICE").Select(_ => IRCMessageType.USERNOTICE))
            .Or(Parse.String("USERSTATE").Select(_ => IRCMessageType.USERSTATE));

        private static readonly Func<Parser<IRCMessageType>, Parser<IRCMessageType>> LineParser = p => from x in Parse.AnyChar.Until(Parse.String(".twitch.tv"))
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
                case IRCMessageType.USERNOTICE:
                    {
                        return new ParseNoticeMessage().Do(input);
                    }
                case IRCMessageType.PRIVMSG:
                    {
                        return new ParsePrivMessage().Do(input);
                    }
                case IRCMessageType.USERSTATE:
                    {
                        return new ParseUserState().Do(input);
                    }
                case IRCMessageType.PART:
                case IRCMessageType.JOIN:
                    {
                        return new ParseEnterExit(command).Do(input);
                    }
            }

            return null;
        }
    }
}