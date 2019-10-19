﻿using System;

namespace twitchstreambot.Parsing
{
    public class ParseUserState : MessageParser
    {
        public override TwitchMessage Do(string input)
        {
            var headers = GetHeaders(input);

            return new TwitchMessage
            {
                User = TwitchMessage.UserFromHeaders(headers),
                MessageType = IRCMessageType.USERSTATE,
                Headers = headers
            };
        }
    }
}