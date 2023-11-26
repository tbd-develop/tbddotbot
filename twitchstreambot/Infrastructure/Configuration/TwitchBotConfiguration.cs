namespace twitchstreambot.Infrastructure.Configuration;

public class TwitchBotConfiguration
{
    public string AuthToken { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Refresh { get; set; } = null!;
}