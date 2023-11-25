namespace twitchstreambot.Infrastructure;

public interface IMessageDispatcher
{
    public MessageResult Dispatch(string message);
}