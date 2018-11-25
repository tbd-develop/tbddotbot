using System;
using System.Collections.Generic;

namespace twitchbot
{
    public class MessageReceivedArgs : EventArgs
    {
        public string UserName { get; }
        public string Message { get; }
        public IDictionary<string, string> Headers { get; }

        public MessageReceivedArgs(string userName, string message, IDictionary<string, string> headers)
        {
            Headers = headers;
            Message = message;
            UserName = userName;
        }
    }
}