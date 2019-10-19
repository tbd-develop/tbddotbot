using System;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchstreambot.infrastructure;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandSet
{
    [TestFixture]
    public class when_registry_provided_and_command_present
    {
        private CommandSet Subject;
        private TwitchMessage Message;
        private Mock<ICommandRegistry> Registry;
        private string AcceptedCommand = "sample";

        [SetUp]
        public void SetUp()
        {
            Registry = new Mock<ICommandRegistry>();
            Registry.Setup(rg => rg.CanProvide(It.Is<string>(s => s == AcceptedCommand))).Returns(true);
            Registry.Setup(rg => rg.Get(It.Is<string>(s => s == AcceptedCommand))).Returns(typeof(SampleCommand));

            Subject = new CommandSet(new[] { Registry.Object });
            Message = new TwitchMessage { Command = new BotCommand() { Action = AcceptedCommand } };
        }

        [Test]
        public void returns_type_of_sample_command()
        {
            Subject.GetCommand(Message).Should().Be<SampleCommand>();

            Registry.Verify(rg => rg.CanProvide(It.Is<string>(s => s == AcceptedCommand)), Times.Once);
            Registry.Verify(rg => rg.Get(It.Is<string>(s => s == AcceptedCommand)), Times.Once);
        }
    }

    public interface ICommandRegistry
    {
        bool CanProvide(string command);
        Type Get(string command);
    }

    public class SampleCommand : ITwitchCommand
    {
        public bool CanExecute()
        {
            throw new System.NotImplementedException();
        }

        public string Execute(params string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}