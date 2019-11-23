using System;
using twitchstreambot.Infrastructure.Configuration;

namespace twitchstreambot.command
{
    public static class BotBuilderCommands
    {
        public static TwitchBotBuilder Configure(this TwitchBotBuilder builder, Action<TwitchBotBuilder> action)
        {
            action(builder);

            return builder;
        }
    }
}