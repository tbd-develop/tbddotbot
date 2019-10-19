using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure.DependencyInjection;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandExecutor
{
    [TestFixture]
    public class when_no_commands_registered
    {
        private CommandDispatcher Subject;
        private Mock<IContainer> Container;
        private Mock<ICommandSet> CommandSet;

        [SetUp]
        public void SetUp()
        {
            Container = new Mock<IContainer>();

            CommandSet = new Mock<ICommandSet>();
            CommandSet.Setup(cmd => cmd.GetCommand(It.IsAny<TwitchMessage>())).Returns(() => null);

            Subject = new CommandDispatcher(CommandSet.Object, Container.Object);
        }

        [Test]
        public void null_value_is_returned()
        {
            Subject.SendTwitchCommand(new TwitchMessage { }).Should().BeNull();
        }
    }
}