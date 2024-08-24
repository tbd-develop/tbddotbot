namespace twitchstreambot.webhooks.Infrastructure.Interim;

public class EntitlementCondition
{
    public string OrganizationId { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string CampaignId { get; set; } = null!;
}