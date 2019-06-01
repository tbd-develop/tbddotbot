using System;
using twitchstreambot.Parsing;

namespace twitchstreambot.infrastructure
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