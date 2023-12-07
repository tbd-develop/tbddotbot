using FluentAssertions;
using twitchstreambot.Parsing.IRCCommands;
using Xunit;

namespace twitchstreambot.tests.ConcerningMessageParsing
{
    public class when_command_contains_url
    {
        private readonly ParsePrivateMessage Subject;

        private readonly string MessageStarter =
            @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user@user.tmi.twitch.tv PRIVMSG #user :";

        private readonly string MessageContent = "!test there is a url here http://test.twitch.tv/gohere";
        private readonly string MessageToParse;

        public when_command_contains_url()
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

        [Fact]
        public void url_is_available_in_arguments()
        {
            var result = Subject.Do(MessageToParse);

            result!.Command!.Arguments.Should().Contain("http://test.twitch.tv/gohere");
        }
    }
}