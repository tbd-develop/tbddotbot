using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace twitchstreambot.Infrastructure
{
    internal class RetrievedMessage
    {
        private const string _commmandPattern = @"^!(?<command>[\w]+){1}(\s(?<arguments>.*))?$";

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