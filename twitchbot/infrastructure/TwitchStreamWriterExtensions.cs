﻿using System.IO;

namespace twitchbot.infrastructure
{
    public static class TwitchStreamWriterExtensions
    {
        public static void SendMessage(this StreamWriter writer, string channel, string message)
        {
            writer.WriteLine($"PRIVMSG #{channel} :{message}");
            writer.Flush();
        }

        public static void SendCommand(this StreamWriter writer, string command)
        {
            writer.WriteLine(command);
            writer.Flush();
        }
    }
}