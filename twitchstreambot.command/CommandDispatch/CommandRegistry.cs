using System;
using Microsoft.Extensions.DependencyInjection;

namespace twitchstreambot.command.CommandDispatch
{
    public abstract class CommandRegistry
    {
        protected IServiceCollection ServiceCollection;

        protected CommandRegistry(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public abstract bool CanProvide(string command);
        public abstract Type Get(string command);
    }
}