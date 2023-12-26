namespace twitchstreambot.webhooks.Events.Contracts;

public interface IContainModeratorInformation
{
    string? ModeratorUserId { get; set; }
    string? ModeratorUserName { get; set; }
    string? ModeratorUserLogin { get; set; }
}