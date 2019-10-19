using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchbot.tests.ConcerningCommandExecutor.Commands;
using twitchbot.tests.ConcerningCommandSet;
using twitchbot.tests.ConcerningCommandSet.Commands;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure.DependencyInjection;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandExecutor
{
    [TestFixture]
    public class when_command_is_registered
    {
        private CommandDispatcher Subject;
        private Mock<ICommandSet> CommandSet;
        private Mock<IContainer> Container;
        private string CommandToExecute = "basic";
        private string ContentToReturn = "ResultingMessage";

        [SetUp]
        public void SetUp()
        {
            Container = new Mock<IContainer>();
            CommandSet = new Mock<ICommandSet>();
            CommandSet.Setup(cs => cs.GetCommand(It.Is<TwitchMessage>(m => m.Command.Action == CommandToExecute)))
                .Returns(typeof(BasicCommand));

            Container.Setup(ctn => ctn.GetInstance(typeof(BasicCommand))).Returns(new BasicCommand(ContentToReturn));

            Subject = new CommandDispatcher(CommandSet.Object, Container.Object);
        }

        [Test]
        public void returned_value_is_content_of_command()
        {
            Subject.SendTwitchCommand(new TwitchMessage
            {
                Command = new BotCommand { Action = CommandToExecute }
            }).Should().Be(ContentToReturn);

            Container.Verify(ctn => ctn.GetInstance(It.Is<Type>(t => t == typeof(BasicCommand))), Times.Once);
        }
    }
}