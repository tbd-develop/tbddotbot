using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandExecutor
{
    [TestFixture]
    public class when_no_commands_registered
    {
        private CommandExecutor Subject;

        [SetUp]
        public void SetUp()
        {
            Subject = new CommandExecutor();
        }

        [Test]
        public void null_value_is_returned()
        {
            Subject.SendTwitchCommand(new TwitchMessage { }).Should().BeNull();
        }
    }

    public class CommandExecutor
    {
        public string SendTwitchCommand(TwitchMessage twitchMessage)
        {
            return twitchMessage.Command?.Action;
        }
    }
}