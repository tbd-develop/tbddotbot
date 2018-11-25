using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace twitchstreambot.infrastructure
{
    public class ChannelReader : StreamReader
    {
        public delegate void CommandReceivedHandler(ChannelReader sender, CommandArgs args);

        public delegate void MessageReceivedHandler(ChannelReader sender, MessageReceivedArgs args);

        public event CommandReceivedHandler OnCommandReceived;
        public event MessageReceivedHandler OnMessageReceived;

        private const string _tagsContentPattern = @"(?<name>[\w-]*)=(?<value>[\/\w\d\s,#-]*)*";

        private const string _messagePattern =
            @"(.+)\s:(?<user>[\w\d]*)\!([\w\d]*@[\w\d]*\.tmi\.twitch\.tv\s)(?<command>[\w]*)\s#(?<channel>[\w\d]*)\s:(?<message>.*)";

        public ChannelReader(Stream stream) : base(stream)
        {
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
            string buffer = string.Empty;

            while ((buffer = ReadLine()) != null)
            {
                var message = ExtractMessageInformation(buffer);

                if (message.IsCommand)
                {
                    var command = message.GetCommand();

                    OnCommandReceived?.Invoke(this,
                        new CommandArgs(Int32.Parse(message.Headers["user-id"]), message.UserName, command.name)
                        {
                            Arguments = command.arguments,
                            Headers = message.Headers
                        });
                }
                else
                {
                    OnMessageReceived?.Invoke(this,
                        new MessageReceivedArgs(message.UserName, message.Content, message.Headers));
                }
            }
        }

        private static RetrievedMessage ExtractMessageInformation(string buffer)
        {
            RetrievedMessage message = new RetrievedMessage();

            MatchCollection matches = Regex.Matches(buffer, _tagsContentPattern);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    message.Headers.Add(match.Groups["name"].Value.Trim(),
                        match.Groups["value"].Captures[0].Value.Trim());
                }
            }

            Match messageBody = Regex.Match(buffer, _messagePattern);

            if (messageBody.Success)
            {
                message.UserName = messageBody.Groups["user"].Value;
                message.IRCCommand = messageBody.Groups["command"].Value;
                message.Content = messageBody.Groups["message"].Value;
            }

            return message;
        }
    }

    internal class RetrievedMessage
    {
        private const string _commmandPattern = @"!(?<command>[\w]+){1}(\s(?<arguments>.*))?$";

        public IDictionary<string, string> Headers { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public string IRCCommand { get; set; }
        public bool IsCommand => !string.IsNullOrEmpty(Content) && Regex.IsMatch(Content, _commmandPattern);

        public RetrievedMessage()
        {
            Headers = new Dictionary<string, string>();
        }

        public (string name, string[] arguments) GetCommand()
        {
            var match = Regex.Match(Content, _commmandPattern);

            if (match.Success)
            {
                return (match.Groups["command"].Value, match.Groups["arguments"].Value.Split(' '));
            }

            return ("", new[] {""});
        }
    }
}