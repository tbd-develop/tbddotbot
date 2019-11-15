using System.Text.RegularExpressions;

namespace twitchstreambot.Parsing.IRCCommands
{
    public class ParsePrivateMessage : MessageParser
    {
        public override TwitchMessage Do(string input)
        {
            string[] components = Regex.Split(input, @":(.*!.*@.*).tmi.twitch.tv\s*");

            string[] messageContents = Regex.Split(components[2], @"\s:");

            var headers = GetHeaders(input);

            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                MessageType = IRCMessageType.PRIVMSG,
                Command = GetCommand(messageContents[1]),
                Headers = headers,
                Content = messageContents[1]
            };
        }

        private BotCommand GetCommand(string input)
        {
            if (input.StartsWith("!"))
            {
                string action = GetEverythingUpToFirstSpace(input);
                string[] arguments = GetEverythingAfterTheFirstSpace(input);

                return new BotCommand { Action = action, Arguments = arguments };
            }

            return null;
        }

        private static string[] GetEverythingAfterTheFirstSpace(string input)
        {
            return input.Substring(input.IndexOf(' ') + 1).Split(' ');
        }

        private static string GetEverythingUpToFirstSpace(string input)
        {
            return input.Substring(1, input.IndexOf(' ') - 1);
        }
    }
}