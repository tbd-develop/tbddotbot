using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure;
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
                _bot.SendToStream(_dispatcher.ExecuteTwitchCommand(message));
            }
        }

        public bool CanExecute(TwitchMessage message)
        {
            return message.IsBotCommand && _dispatcher.CanExecute(message);
        }
    }
}