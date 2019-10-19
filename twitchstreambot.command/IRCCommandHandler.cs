using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.@new;
using twitchstreambot.Parsing;

namespace twitchstreambot.command
{
    public class IRCCommandHandler : IRCHandler
    {
        private readonly CommandDispatcher _dispatcher;
        private readonly TwitchStreamBot _bot;

        public IRCCommandHandler(CommandDispatcher dispatcher, TwitchStreamBot bot)
        {
            _dispatcher = dispatcher;
            _bot = bot;
        }

        public void Handle(TwitchMessage message)
        {
            if (message.IsBotCommand)
            {
                _bot.SendToStream(_dispatcher.SendTwitchCommand(message));
            }
        }
    }
}