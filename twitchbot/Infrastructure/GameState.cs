using System.IO;
using System.Linq;
using System.Text.Json;
using twitchbot.Games.Models;

namespace twitchbot.Infrastructure;

public class GameState
{
    public bool IsStarted { get; private set; }
    public string CurrentPlayer { get; private set; } = null!;

    private Game _currentGame = null!;
    private string _currentRoom = null!;

    public void Start(string userIdentifier)
    {
        IsStarted = true;
        CurrentPlayer = userIdentifier;
        _currentGame =
            JsonSerializer.Deserialize<Game>(File.ReadAllText("Games/game1.json"), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;

        _currentRoom = _currentGame.Starting;
    }

    public string GetRoomDescription() => _currentGame.Rooms[_currentRoom].Description;

    public string? ProcessCommand(string command)
    {
        var currentRoom = _currentGame.Rooms[_currentRoom];

        if (currentRoom.Commands.FirstOrDefault(c => c.Aliases.Contains(command)) is { } response)
        {
            return response.Description;
        }

        return null;
    }
}