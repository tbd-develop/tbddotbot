using System;
using System.Collections.Generic;

namespace twitchstreambot.infrastructure
{
    public class MessageReceivedArgs : EventArgs
    {
        public string Message { get; }

        public MessageReceivedArgs(string message)
        {
            Message = message;
        }
    }
}