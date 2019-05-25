using Topshelf;

namespace twitchbot
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(cfg =>
            {
                cfg.Service<BotService>(svc =>
                {
                    svc.ConstructUsing(() => new BotService());
                    svc.WhenStarted(s => s.Start());
                    svc.WhenStopped(s => s.Stop());
                });

                cfg.SetDescription("Twitch Channel Bot");
                cfg.SetDisplayName("TBDDOTBOT");
                cfg.SetServiceName("TwitchChannelBot");
                cfg.SetInstanceName("tbddotbot");
            });
        }
    }
}