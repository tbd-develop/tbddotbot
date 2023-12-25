using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace twitchstreambot.pubsub.Messages
{
    public class Redemption
    {
        public Guid Id { get; set; }
        public TwitchUserResponse User { get; set; } = null!;

        [JsonPropertyName("channel_id")] public long ChannelId { get; set; }
        [JsonPropertyName("redeemed_at")] public DateTime RedeemedAt { get; set; }

        public RedemptionReward Reward { get; set; } = null!;

        [JsonPropertyName("user_input")] public string UserInput { get; set; } = null!;
        public string Status { get; set; } = null!;

        public class RedemptionReward
        {
            public Guid Id { get; set; }
            [JsonPropertyName("channel_id")] public long ChannelId { get; set; }
            public string Title { get; set; } = null!;
            public string Prompt { get; set; } = null!;
            public int Cost { get; set; }

            [JsonPropertyName("is_user_input_required")]
            public bool IsUserInputRequired { get; set; }

            [JsonPropertyName("is_sub_only")] public bool IsSubOnly { get; set; }
            public IDictionary<string, string> Image { get; set; } = null!;
            [JsonPropertyName("default_image")] public IDictionary<string, string> DefaultImage { get; set; } = null!;
            [JsonPropertyName("background_color")] public string BackgroundColor { get; set; } = null!;
            [JsonPropertyName("is_enabled")] public bool IsEnabled { get; set; }
            [JsonPropertyName("is_paused")] public bool IsPaused { get; set; }
            [JsonPropertyName("is_in_stock")] public bool IsInStock { get; set; }
            [JsonPropertyName("max_per_stream")] public StreamMaximum MaximumPerStream { get; set; } = null!;

            [JsonPropertyName("should_redemptions_skip_request_queue")]
            public bool ShouldRedemptionsSkipRequestQueue { get; set; }
        }

        public class StreamMaximum
        {
            [JsonPropertyName("is_enabled")] public bool IsEnabled { get; set; }
            [JsonPropertyName("max_per_stream")] public int Count { get; set; }
        }
    }
}