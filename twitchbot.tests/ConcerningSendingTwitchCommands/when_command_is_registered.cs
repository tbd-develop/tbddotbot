using System.Runtime.InteropServices;
using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandExecutor
{
    [TestFixture]
    public class when_command_is_registered
    {
        private CommandExecutor Subject;

        [SetUp]
        public void SetUp()
        {
            Subject = new CommandExecutor();
        }

        [Test]
        public void returned_value_is_not_null()
        {
            Subject.SendTwitchCommand(new TwitchMessage
            {
                Command = new BotCommand {  Action = "saysomething" }
            }).Should().NotBeNullOrEmpty();
        }
    }
}