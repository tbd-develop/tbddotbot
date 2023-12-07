using System;

namespace twitchstreambot.Infrastructure;

public interface ICommandLookup
{
    bool TryGetCommand(string action, out Type? commandType);
}