using System.IO;
using System.Text;

namespace twitchstreambot.Infrastructure.Communications
{
    public class ChannelReader : StreamReader
    {
        public delegate void MessageReceivedHandler(ChannelReader sender, MessageReceivedArgs args);

        public delegate void ReaderShutdownHandler(ChannelReader sender);

        public event MessageReceivedHandler OnMessageReceived;
        public event ReaderShutdownHandler OnReaderShutdown;

        private bool _exiting;

        public ChannelReader(Stream stream) : base(stream)
        {
            _exiting = false;
        }

        public ChannelReader(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream,
            detectEncodingFromByteOrderMarks)
        {
        }

        public ChannelReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public ChannelReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(stream,
            encoding, detectEncodingFromByteOrderMarks)
        {
        }

        public ChannelReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) :
            base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
        }

        public ChannelReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize,
            bool leaveOpen) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
        {
        }

        public ChannelReader(string path) : base(path)
        {
        }

        public ChannelReader(string path, bool detectEncodingFromByteOrderMarks) : base(path,
            detectEncodingFromByteOrderMarks)
        {
        }

        public ChannelReader(string path, Encoding encoding) : base(path, encoding)
        {
        }

        public ChannelReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(path,
            encoding, detectEncodingFromByteOrderMarks)
        {
        }

        public ChannelReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) :
            base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
        }

        public void ListenForMessages()
        {
            string buffer;

            while ((buffer = ReadLine()) != null && !_exiting)
            {
                OnMessageReceived?.Invoke(this, new MessageReceivedArgs(buffer));
            }
        }

        public void SignalShutdown()
        {
            _exiting = true;

            OnReaderShutdown?.Invoke(this);
        }
    }
}