using FluentAssertions;
using twitchstreambot.Parsing.IRCCommands;
using Xunit;

namespace twitchstreambot.tests.ConcerningMessageParsing
{
    public class when_command_has_no_arguments
    {
        public ParsePrivateMessage Subject;
        public string MessageStarter = @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user@user.tmi.twitch.tv PRIVMSG #user :";
        public string MessageContent = "!test";
        public string MessageToParse;

        public when_command_has_no_arguments()
        {
            MessageToParse = $"{MessageStarter}{MessageContent}";
            Subject = new ParsePrivateMessage();
        }

        [Fact]
        public void is_identified_as_bot_command()
        {
            var result = Subject.Do(MessageToParse);

            result!.IsBotCommand.Should().BeTrue();
        }

        [Fact]
        public void message_is_intact()
        {
            var result = Subject.Do(MessageToParse);

            result!.Content.Should().Be(MessageContent);
        }
    }
}