using System;
using System.Collections.Generic;

namespace twitchstreambot.infrastructure
{
    public class CommandArgs : EventArgs
    {
        public int UserId { get; }
        public string UserName { get; }
        public string Command { get; }
        public IEnumerable<string> Arguments { get; set; }
        public IDictionary<string, string> Headers { get; set; }

        public CommandArgs(int userId, string user, string command)
        {
            UserId = userId;
            UserName = user;
            Command = command;
        }
    }
}