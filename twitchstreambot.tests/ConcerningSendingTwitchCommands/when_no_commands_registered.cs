using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure.DependencyInjection;
using twitchstreambot.Parsing;

namespace twitchstreambot.tests.ConcerningSendingTwitchCommands
{
    [TestFixture]
    public class when_no_commands_registered
    {
        private CommandDispatcher Subject;
        private Mock<IServiceProvider> Container;
        private Mock<ICommandSet> CommandSet;

        [SetUp]
        public void SetUp()
        {
            Container = new Mock<IServiceProvider>();

            CommandSet = new Mock<ICommandSet>();
            CommandSet.Setup(cmd => cmd.GetCommand(It.IsAny<TwitchMessage>())).Returns(() => null);

            Subject = new CommandDispatcher(CommandSet.Object, Container.Object);
        }

        [Test]
        public void null_value_is_returned()
        {
            Subject.ExecuteTwitchCommand(new TwitchMessage { }).Should().BeNull();
        }
    }
}