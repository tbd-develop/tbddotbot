using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using twitchstreambot.Parsing;

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

    public class CommandSet
    {
        private readonly ICommandRegistry[] _registries;

        public CommandSet(ICommandRegistry[] registries)
        {
            Guard.IsNotEmpty(registries, "At least one registry must be provided for commands");

            _registries = registries;
        }

        public Type GetCommand(TwitchMessage message)
        {
            var registry = _registries.SingleOrDefault(r => r.CanProvide(message.Command.Action));

            return registry?.Get(message.Command.Action);
        }
    }

    public class Guard
    {
        public static void IsNotEmpty<T>(IEnumerable<T> source, string message)
        {
            if (source == null || !source.Any())
            {
                throw new ArgumentException(message);
            }
        }
    }
}