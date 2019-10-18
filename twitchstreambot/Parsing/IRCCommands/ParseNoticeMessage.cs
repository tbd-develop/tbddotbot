namespace twitchstreambot.Parsing
{
    public class ParseNoticeMessage : MessageParser
    {
        public override TwitchMessage Do(string input)
        {
            var headers = GetHeaders(input);

            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                Headers = headers,
                IrcCommand = TwitchCommand.USERNOTICE
            };
        }
    }
}