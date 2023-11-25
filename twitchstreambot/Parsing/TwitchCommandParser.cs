using System;
using System.ComponentModel;
using Sprache;
using twitchstreambot.Parsing.IRCCommands;

namespace twitchstreambot.Parsing
{
    public class TwitchCommandParser
    {
        private static readonly Parser<IRCMessageType> CommandParser =
            Parse.String("PRIVMSG").Select(_ => IRCMessageType.PrivateMessage)
                .Or(Parse.String("JOIN").Select(_ => IRCMessageType.Join))
                .Or(Parse.String("PART").Select(_ => IRCMessageType.Part))
                .Or(Parse.String("USERNOTICE").Select(_ => IRCMessageType.UserNotice))
                .Or(Parse.String("USERSTATE").Select(_ => IRCMessageType.UserState));

        private static readonly Func<Parser<IRCMessageType>, Parser<IRCMessageType>> LineParser =
            p => from x in Parse.AnyChar.Until(Parse.String(".twitch.tv"))
                from l in Parse.WhiteSpace
                from c in p
                from lr in Parse.WhiteSpace
                from r in Parse.AnyChar.Many()
                select c;

        private static readonly Func<string, bool> IsMatch = input =>
            LineParser(CommandParser).TryParse(input).WasSuccessful;

        public static bool TryMatch(string input, out TwitchMessage? message)
        {
            var result = IsMatch(input);

            message = result ? Gather(input) : default;

            return result;
        }

        private static TwitchMessage? Gather(string input)
        {
            var command = LineParser(CommandParser).Parse(input);

            switch (command)
            {
                case IRCMessageType.UserNotice:
                {
                    return new ParseNoticeMessage().Do(input);
                }
                case IRCMessageType.PrivateMessage:
                {
                    return new ParsePrivateMessage().Do(input);
                }
                case IRCMessageType.UserState:
                {
                    return new ParseUserState().Do(input);
                }
                case IRCMessageType.Part:
                case IRCMessageType.Join:
                {
                    return new ParseEnterExit(command).Do(input);
                }
                case IRCMessageType.None:
                    break;
                default:
                    throw new InvalidEnumArgumentException("Invalid IRCMessageType");
            }

            return null;
        }
    }
}