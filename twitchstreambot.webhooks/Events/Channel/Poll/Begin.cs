using System.Text.Json.Serialization;
using twitchstreambot.webhooks.Events.Contracts;
using twitchstreambot.webhooks.Events.Values;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Poll;

[WebhookEvent("channel.poll.begin")]
public class Begin : PollStatus
{
}