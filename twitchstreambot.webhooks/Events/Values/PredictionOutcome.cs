using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class PredictionOutcome
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Color { get; set; } = null!;
    public int Users { get; set; }
    [JsonPropertyName("channel_points")] public int ChannelPoints { get; set; }
    [JsonPropertyName("top_predictors")] public TopPredictor[] TopPredictors { get; set; } = null!;
}