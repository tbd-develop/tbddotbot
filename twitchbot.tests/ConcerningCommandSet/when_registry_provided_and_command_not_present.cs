using FluentAssertions;
using Moq;
using NUnit.Framework;
using twitchstreambot.Infrastructure.@new;
using twitchstreambot.Parsing;

namespace twitchbot.tests.ConcerningCommandSet
{
    [TestFixture]
    public class when_registry_provided_and_command_not_present
    {
        private CommandSet Subject;
        private TwitchMessage Message;
        private Mock<ICommandRegistry> Registry;
        private string NotPresentCommand;

        [SetUp]
        public void SetUp()
        {
            NotPresentCommand = "notpresent";
            Registry = new Mock<ICommandRegistry>();
            Registry.Setup(rg => rg.CanProvide(It.Is<string>(s => s == NotPresentCommand))).Returns(false);

            Subject = new CommandSet(new[] { Registry.Object });

            Message = new TwitchMessage() { Command = new BotCommand() { Action = NotPresentCommand } };
        }

        [Test]
        public void comand_is_not_retrieved()
        {
            Subject.GetCommand(Message).Should().BeNull();

            Registry.Verify(rg => rg.CanProvide(It.Is<string>(s => s == NotPresentCommand)), Times.Once);
            Registry.Verify(rg => rg.Get(It.Is<string>(s => s == NotPresentCommand)), Times.Never);
        }
    }
}