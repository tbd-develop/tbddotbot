using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace twitchstreambot.Infrastructure
{
    public class HelixCollectionResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;

        public bool HasData => Data?.Count() > 0;
    }

    public class HelixWebHookCollectionResponse<T> : HelixCollectionResponse<T>
    {
        public int Total { get; set; }
        [JsonPropertyName("total_cost")] public int TotalCost { get; set; }
        [JsonPropertyName("max_total_cost")] public int MaximumTotalCost { get; set; }
    }
}