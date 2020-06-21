using System;

namespace twitchstreambot.pubsub.Messages.Data
{
    public class RewardRedeemedData
    {
        public DateTime TimeStamp { get; set; }
        public Redemption Redemption { get; set; }
    }
}