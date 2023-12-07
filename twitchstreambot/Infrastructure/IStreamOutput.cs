namespace twitchstreambot.Infrastructure;

public interface IStreamOutput
{
    void SendToStream(string message);
}