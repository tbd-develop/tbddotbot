using System;
using twitchstreambot.Infrastructure.Configuration;

namespace twitchstreambot.command
{
    public static class BotBuilderCommands
    {
        public static TwitchBotBuilder WithCommands(this TwitchBotBuilder builder, Action<TwitchBotBuilder> action)
        {
            action(builder);

            return builder;
        }
    }
}