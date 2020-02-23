using System.Collections.Generic;

namespace twitchstreambot.api.Infrastructure
{
    public class HelixCollectionResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}