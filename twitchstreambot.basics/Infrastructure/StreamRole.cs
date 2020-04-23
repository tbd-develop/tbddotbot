namespace twitchstreambot.basics.Infrastructure
{
    public class StreamRole
    {
        public static StreamRole Moderator = new StreamRole("moderator");
        public static StreamRole Broadcaster = new StreamRole("broadcaster");

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
}