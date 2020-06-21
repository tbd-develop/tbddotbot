using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.pubsub.Messages
{
    public class Redemption
    {
        public Guid Id { get; set; }
        public TwitchUserResponse User { get; set; }
        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }
        [JsonProperty("redeemed_at")]
        public DateTime RedeemedAt { get; set; }
        public RedemptionReward Reward { get; set; }
        [JsonProperty("user_input")]
        public string UserInput { get; set; }
        public string Status { get; set; }

        public class RedemptionReward
        {
            public Guid Id { get; set; }
            [JsonProperty("channel_id")]
            public long ChannelId { get; set; }
            public string Title { get; set; }
            public string Prompt { get; set; }
            public int Cost { get; set; }
            [JsonProperty("is_user_input_required")]
            public bool IsUserInputRequired { get; set; }
            [JsonProperty("is_sub_only")]
            public bool IsSubOnly { get; set; }
            public IDictionary<string, string> Image { get; set; }
            [JsonProperty("default_image")]
            public IDictionary<string, string> DefaultImage { get; set; }
            [JsonProperty("background_color")]
            public string BackgroundColor { get; set; }
            [JsonProperty("is_enabled")]
            public bool IsEnabled { get; set; }
            [JsonProperty("is_paused")]
            public bool IsPaused { get; set; }
            [JsonProperty("is_in_stock")]
            public bool IsInStock { get; set; }
            [JsonProperty("max_per_stream")]
            public StreamMaximum MaximumPerStream { get; set; }
            [JsonProperty("should_redemptions_skip_request_queue")]
            public bool ShouldRedemptionsSkipRequestQueue { get; set; }
        }

        public class StreamMaximum
        {
            [JsonProperty("is_enabled")]
            public bool IsEnabled { get; set; }
            [JsonProperty("max_per_stream")]
            public int Count { get; set; }
        }
    }
}