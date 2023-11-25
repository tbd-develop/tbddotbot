using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Parsing;

namespace twitchstreambot.tests.ConcerningSendingTwitchCommands
{
    [TestFixture]
    public class when_no_commands_registered
    {
        private CommandDispatcher Subject;
        private IServiceProvider Container;
        private ICommandSet CommandSet;

        [SetUp]
        public void SetUp()
        {
            Container = Substitute.For<IServiceProvider>();

            CommandSet = Substitute.For<ICommandSet>();

            CommandSet
                .GetCommandType(Arg.Any<TwitchMessage>())
                .Returns((Type)null);

            Subject = new CommandDispatcher(CommandSet, Container);
        }

        [Test]
        public void null_value_is_returned()
        {
            Subject
                .ExecuteTwitchCommand(new TwitchMessage { })
                .Should()
                .BeNull();
        }
    }
}