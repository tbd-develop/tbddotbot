using System.Collections.Generic;

namespace twitchbot.Games.Models;

public class Command
{
    public IEnumerable<string> Aliases { get; set; } = null!;
    public IEnumerable<string>? Requires { get; set; } 
    public string Description { get; set; } = null!;
}