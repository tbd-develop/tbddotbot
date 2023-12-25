namespace twitchstreambot.Api.Requests.WebHook;

public class SubscriptionTransportDefinition
{
    public string Method { get; set; } = null!;
    public string Callback { get; set; } = null!;
    public string Secret { get; set; } = null!;
}