using System.Collections.Generic;

namespace twitchstreambot.Parsing;

public class BotCommand
{
    public string Action { get; set; } = null!;
    public IEnumerable<string> Arguments { get; set; } = null!;
}