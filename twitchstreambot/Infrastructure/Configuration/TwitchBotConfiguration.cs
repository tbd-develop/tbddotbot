using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure.Configuration
{
    public class TwitchBotConfiguration
    {
        private readonly IDictionary<IRCMessageType, List<Type>> _handlers;

        public IReadOnlyDictionary<IRCMessageType, List<Type>> Handlers =>
            new ReadOnlyDictionary<IRCMessageType, List<Type>>(_handlers);

        public TwitchConnection Connection { get; private set; }
        public string AuthToken { get; private set; }

        public TwitchBotConfiguration()
        {
            _handlers = new Dictionary<IRCMessageType, List<Type>>();
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
            if (_handlers.ContainsKey(messageType))
            {
                _handlers[messageType].Add(handlerType);
            }
            else
            {
                _handlers.Add(messageType, new List<Type> { handlerType });
            }
        }
    }
}