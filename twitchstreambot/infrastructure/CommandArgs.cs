using System;
using twitchstreambot.Parsing;

namespace twitchstreambot.Infrastructure
{
    public class CommandArgs : EventArgs
    {
        public TwitchMessage Message { get; }

        public CommandArgs(TwitchMessage message)
        {
            Message = message;
        }
    }
}