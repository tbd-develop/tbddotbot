namespace twitchstreambot.webhooks.Events.Contracts;

public interface IContainUserInformation
{
    string? UserId { get; set; }
    string? UserName { get; set; }
    string? UserLogin { get; set; }
}