using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.Configuration
{
    public class TwitchBotBuilder
    {
        private readonly TwitchBotConfiguration _configuration;

        public TwitchBotBuilder()
        {
            _configuration = new TwitchBotConfiguration();
        }

        public TwitchBotBuilder WithConnection(TwitchConnection connection)
        {
            _configuration.SetConnection(connection);

            return this;
        }

        public TwitchBotBuilder WithAuthentication(string authToken)
        {
            _configuration.SetAuthentication(authToken);

            return this;
        }

        public TwitchBotBuilder AddHandler<THandler>(IRCMessageType key)
        {
            _configuration.AddHandler(key, typeof(THandler));

            return this;
        }

        public TwitchBotConfiguration Build()
        {
            return _configuration;
        }
    }
}