using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Parsing.IRCCommands;

namespace twitchstreambot.tests.ConcerningMessageParsing
{
    [TestFixture]
    public class when_command_contains_url
    {
        public ParsePrivateMessage Subject;
        public string MessageStarter = @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user@user.tmi.twitch.tv PRIVMSG #user :";
        public string MessageContent = "!test there is a url here http://test.twitch.tv/gohere";
        public string MessageToParse;

        [SetUp]
        public void SetUp()
        {
            MessageToParse = $"{MessageStarter}{MessageContent}";
            Subject = new ParsePrivateMessage();
        }

        [Test]
        public void is_identified_as_bot_command()
        {
            var result = Subject.Do(MessageToParse);

            result.IsBotCommand.Should().BeTrue();
        }

        [Test]
        public void message_is_intact()
        {
            var result = Subject.Do(MessageToParse);

            result.Content.Should().Be(MessageContent);
        }

        [Test]
        public void url_is_available_in_arguments()
        {
            var result = Subject.Do(MessageToParse);

            result.Command.Arguments.Should().Contain("http://test.twitch.tv/gohere");
        }
    }
}