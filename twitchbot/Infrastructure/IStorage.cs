namespace twitchbot.Infrastructure
{
    public interface IStorage
    {
        void Store<T>(T entity);
    }
}