using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Parsing.IRCCommands;

namespace twitchstreambot.tests.ConcerningMessageParsing
{
    [TestFixture]
    public class when_message_contains_url
    {
        public ParsePrivateMessage Subject;
        public string MessageStarter = @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user@user.tmi.twitch.tv PRIVMSG #user :";
        public string MessageContent = "Test http://twitch.tv is a url";
        public string MessageToParse;

        [SetUp]
        public void SetUp()
        {
            MessageToParse = $"{MessageStarter}{MessageContent}";
            Subject = new ParsePrivateMessage();
        }

        [Test]
        public void message_is_not_treated_as_bot_message()
        {
            var result = Subject.Do(MessageToParse);

            result.IsBotCommand.Should().BeFalse();
            result.Command.Should().BeNull();
        }

        [Test]
        public void message_is_intact()
        {
            var result = Subject.Do(MessageToParse);

            result.Content.Should().Be(MessageContent);
        }
    }
}