using System.IO;
using System.Text;

namespace twitchbot
{
    public class ChannelWriter : StreamWriter
    {
        public string Channel { get; set; }
        public string BotName { get; set; }
        public string AuthToken { get; set; }
        
        public ChannelWriter(Stream stream) : base(stream)
        {
        }        

        public ChannelWriter(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public ChannelWriter(Stream stream, Encoding encoding, int bufferSize) : base(stream, encoding, bufferSize)
        {
        }

        public ChannelWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen) : base(stream, encoding, bufferSize, leaveOpen)
        {
        }

        public ChannelWriter(string path) : base(path)
        {
        }

        public ChannelWriter(string path, bool append) : base(path, append)
        {
        }

        public ChannelWriter(string path, bool append, Encoding encoding) : base(path, append, encoding)
        {
        }

        public ChannelWriter(string path, bool append, Encoding encoding, int bufferSize) : base(path, append, encoding, bufferSize)
        {
        }

        public void Authenticate()
        {
            WriteLine($"PASS oauth:{AuthToken}");
            WriteLine($"NICK {BotName}");
            WriteLine($"JOIN #{Channel}");   
        }        

        public void SendMessage(string message)
        {
            WriteLine($"PRIVMSG #{Channel} :{message}");
            Flush();
        }

        public void SendCommand(string command)
        {
            WriteLine(command);
            Flush();
        }
    }
}