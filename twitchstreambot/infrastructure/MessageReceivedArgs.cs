using System;

namespace twitchstreambot.Infrastructure
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