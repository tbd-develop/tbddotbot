﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Parsing;
using twitchstreambot.tests.ConcerningCommandSet.Commands;

namespace twitchstreambot.tests.ConcerningCommandSet
{
    [TestFixture]
    public class when_registry_provided_and_command_present
    {
        private CommandSet Subject;
        private TwitchMessage Message;
        private Mock<CommandRegistry> Registry;
        private string AcceptedCommand = "sample";

        [SetUp]
        public void SetUp()
        {
            Registry = new Mock<CommandRegistry>();
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
}