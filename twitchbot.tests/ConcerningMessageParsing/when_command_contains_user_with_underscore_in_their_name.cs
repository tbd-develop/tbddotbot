using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Parsing.IRCCommands;

namespace twitchbot.tests.ConcerningMessageParsing
{
    [TestFixture]
    public class when_command_contains_user_with_underscore_in_their_name
    {
        public ParsePrivateMessage Subject;

        public string MessageToParse =
            @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user @user.tmi.twitch.tv PRIVMSG #user :@user_name";

        [SetUp]
        public void SetUp()
        {
            Subject = new ParsePrivateMessage();
        }

        [Test]
        public void message_is_complete()
        {
            var message = Subject.Do(MessageToParse);

            message.Content.Should().Be("@user_name");
        }
    }
}