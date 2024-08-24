using System.Collections.Generic;

namespace twitchbot.Games.Models;

public class Game
{
    public string Starting { get; set; }
    public IDictionary<string, Room> Rooms { get; set; } = null!;
}