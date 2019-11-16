using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using twitchstreambot.basics.Infrastructure;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.command.Commands;
using twitchstreambot.models;

namespace twitchstreambot.basics
{
    public class DefinedCommandsRegistry : CommandRegistry
    {
        private readonly DefinedCommandsStore _commandStore;

        public DefinedCommandsRegistry(IServiceCollection serviceCollection, DefinedCommandsStore commandStore) : base(serviceCollection)
        {
            _commandStore = commandStore;

            serviceCollection.AddTransient(typeof(RecallCommand));
        }

        public override bool CanProvide(string command)
        {
            return LoadDefinitions().Any(c => c.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase));
        }

        public override Type Get(string command)
        {
            return typeof(RecallCommand);
        }

        private List<CommandDefinition> LoadDefinitions()
        {
            var currentCommands = _commandStore.Load();

            if (!string.IsNullOrEmpty(currentCommands))
            {
                return new List<CommandDefinition>(
                    JsonConvert.DeserializeObject<IEnumerable<CommandDefinition>>(currentCommands));
            }

            return new List<CommandDefinition>();
        }
    }
}