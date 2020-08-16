using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.command
{
    public class BotCommandsHandler : IRCHandler
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly TwitchStreamBot _bot;

        public BotCommandsHandler(ICommandDispatcher dispatcher, TwitchStreamBot bot)
        {
            _dispatcher = dispatcher;
            _bot = bot;
        }

        public void Handle(TwitchMessage message)
        {
            if (message.IsBotCommand && _dispatcher.CanExecute(message))
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