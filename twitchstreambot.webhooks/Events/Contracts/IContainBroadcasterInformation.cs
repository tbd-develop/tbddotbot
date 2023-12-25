namespace twitchstreambot.webhooks.Events.Contracts;

public interface IContainBroadcasterInformation
{
    string BroadcasterUserId { get; set; }
    string BroadcasterUserName { get; set; }
    string BroadcasterUserLogin { get; set; }
}