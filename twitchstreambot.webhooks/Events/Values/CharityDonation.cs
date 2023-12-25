using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class CharityDonation
{
    [JsonPropertyName("charity_name")] public string CharityName { get; set; } = null!;

    public MonetaryAmount Amount { get; set; } = null!;
}