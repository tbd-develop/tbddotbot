using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Infrastructure.DependencyInjection;
using twitchstreambot.Parsing;
using twitchstreambot.tests.ConcerningSendingTwitchCommands.Commands;

namespace twitchstreambot.tests.ConcerningSendingTwitchCommands
{
    [TestFixture]
    public class when_command_is_registered
    {
        private CommandDispatcher Subject;
        private Mock<ICommandSet> CommandSet;
        private Mock<IServiceProvider> ServiceProvider;
        private string CommandToExecute = "basic";
        private string ContentToReturn = "ResultingMessage";

        [SetUp]
        public void SetUp()
        {
            ServiceProvider = new Mock<IServiceProvider>();
            CommandSet = new Mock<ICommandSet>();
            CommandSet.Setup(cs => cs.GetCommand(It.Is<TwitchMessage>(m => m.Command.Action == CommandToExecute)))
                .Returns(typeof(BasicCommand));

            ServiceProvider.Setup(ctn => ctn.GetService(typeof(BasicCommand)))
                .Returns(new BasicCommand(ContentToReturn));

            Subject = new CommandDispatcher(CommandSet.Object, ServiceProvider.Object);
        }

        [Test]
        public void returned_value_is_content_of_command()
        {
            Subject.ExecuteTwitchCommand(new TwitchMessage
            {
                Command = new BotCommand { Action = CommandToExecute }
            }).Should().Be(ContentToReturn);

            ServiceProvider.Verify(ctn => ctn.GetService(It.Is<Type>(t => t == typeof(BasicCommand))), Times.Once);
        }
    }
}