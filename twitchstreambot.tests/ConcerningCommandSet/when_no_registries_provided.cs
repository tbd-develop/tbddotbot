using System;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;

namespace twitchstreambot.tests.ConcerningCommandSet
{
    [TestFixture]
    public class when_no_registries_provided
    {
        [Test]
        public void exception_is_thrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var commandSet = new CommandSet(null);
            });
        }
    }
}