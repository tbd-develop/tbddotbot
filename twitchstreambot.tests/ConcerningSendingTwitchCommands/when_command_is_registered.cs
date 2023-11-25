using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Parsing;
using twitchstreambot.tests.ConcerningSendingTwitchCommands.Commands;

namespace twitchstreambot.tests.ConcerningSendingTwitchCommands
{
    [TestFixture]
    public class when_command_is_registered
    {
        private CommandDispatcher Subject;
        private ICommandSet CommandSet;
        private IServiceProvider ServiceProvider;
        private string CommandToExecute = "basic";
        private string ContentToReturn = "ResultingMessage";

        [SetUp]
        public void SetUp()
        {
            ServiceProvider = Substitute.For<IServiceProvider>();
            CommandSet = Substitute.For<ICommandSet>();

            CommandSet.GetCommandType(Arg.Is<TwitchMessage>(m => m.Command.Action == CommandToExecute))
                .Returns(typeof(BasicCommand));

            ServiceProvider.GetService(Arg.Is<Type>(t => t == typeof(BasicCommand)))
                .Returns(new BasicCommand(ContentToReturn));

            Subject = new CommandDispatcher(CommandSet, ServiceProvider);
        }

        [Test]
        public void returned_value_is_content_of_command()
        {
            Subject.ExecuteTwitchCommand(new TwitchMessage
            {
                Command = new BotCommand { Action = CommandToExecute }
            }).Should().Be(ContentToReturn);

            ServiceProvider
                .Received(1)
                .GetService(Arg.Is<Type>(t => t == typeof(BasicCommand)));
        }
    }
}