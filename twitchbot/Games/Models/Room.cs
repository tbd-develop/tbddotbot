using System.Collections.Generic;

namespace twitchbot.Games.Models;

public class Room
{
    public string Description { get; set; } = null!;
    public IEnumerable<Command> Commands { get; set; } = null!;
    public Dictionary<string, string> Exits { get; set; } = new();
}