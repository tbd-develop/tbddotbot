namespace twitchstreambot.Infrastructure.Extensions;

public class StreamRole
{
    public static StreamRole Moderator = new StreamRole("moderator");
    public static StreamRole Broadcaster = new StreamRole("broadcaster");
    public static StreamRole Subscriber = new StreamRole("subscriber");
    public static StreamRole Vip = new StreamRole("vip");

    private readonly string _roleIdentifier;

    StreamRole(string roleIdentifier)
    {
        _roleIdentifier = roleIdentifier;
    }

    public static implicit operator string(StreamRole role)
    {
        return role._roleIdentifier;
    }
}