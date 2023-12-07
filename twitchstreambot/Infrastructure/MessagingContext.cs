using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure;

public class MessagingContext
{
    public MessagingContext(TwitchMessage message)
    {
        Message = message;
    }

    public TwitchMessage Message { get; }
}