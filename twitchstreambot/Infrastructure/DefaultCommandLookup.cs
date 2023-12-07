using System;
using System.Collections.Generic;

namespace twitchstreambot.Infrastructure;

public class DefaultCommandLookup : ICommandLookup
{
    private readonly IDictionary<string, Type> _commands;

    public DefaultCommandLookup(IDictionary<string, Type> commands)
    {
        _commands = commands;
    }

    public bool TryGetCommand(string action, out Type? commandType)
    {
        return _commands.TryGetValue(action.ToLower(), out commandType);
    }
}