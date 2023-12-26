using twitchstreambot.Infrastructure;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Attributes;

namespace twitchstreambot.webhooks.Events.Channel.Chat;

[WebhookEvent("channel.chat.clear")]
public class Clear : WebhookFromBroadcasterEvent
{ }