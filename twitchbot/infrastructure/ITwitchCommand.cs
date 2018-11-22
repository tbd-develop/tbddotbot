namespace twitchbot.infrastructure
{
    public interface ITwitchCommand
    {
        string Execute(params string[] args);
    }
}