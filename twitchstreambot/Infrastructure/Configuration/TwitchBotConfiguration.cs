using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.Configuration
{
    public class TwitchBotConfiguration
    {
        private readonly IDictionary<IRCMessageType, Type> _handlers;

        public IReadOnlyDictionary<IRCMessageType, Type> Handlers =>
            new ReadOnlyDictionary<IRCMessageType, Type>(_handlers);

        public TwitchConnection Connection { get; private set; }
        public string AuthToken { get; private set; }

        public TwitchBotConfiguration()
        {
            _handlers = new Dictionary<IRCMessageType, Type>();
        }

        public void SetConnection(TwitchConnection connection)
        {
            Connection = connection;
        }

        public void SetAuthentication(string token)
        {
            AuthToken = token;
        }

        public void AddHandler(IRCMessageType messageType, Type handlerType)
        {
            _handlers.Add(messageType, handlerType);
        }
    }
}