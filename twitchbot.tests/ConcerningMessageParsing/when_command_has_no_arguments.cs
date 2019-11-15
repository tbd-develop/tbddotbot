using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Parsing.IRCCommands;

namespace twitchbot.tests.ConcerningMessageParsing
{
    [TestFixture]
    public class when_command_has_no_arguments
    {
        public ParsePrivateMessage Subject;
        public string MessageStarter = @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user@user.tmi.twitch.tv PRIVMSG #user :";
        public string MessageContent = "!test";
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
    }
}