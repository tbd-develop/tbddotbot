using System.Collections.Generic;

namespace twitchstreambot.Infrastructure
{
    public class HelixCollectionResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}