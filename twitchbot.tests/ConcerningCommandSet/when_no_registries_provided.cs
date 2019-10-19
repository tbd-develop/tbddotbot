using System;
using System.Collections;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using twitchstreambot.command.CommandDispatch;

namespace twitchbot.tests.ConcerningCommandSet
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