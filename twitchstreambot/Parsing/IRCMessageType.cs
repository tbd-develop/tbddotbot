namespace twitchstreambot.Parsing
{
    public enum IRCMessageType
    {
        None,
        PrivateMessage,
        Join,
        Part,
        UserNotice,
        UserState
    }
}