using twitchstreambot.webhooks.Events.User.Authorization;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Publishing;

namespace webhook_testing.Handlers.User;

public class AuthorizationRevokeHandler : TwitchWebhookEventHandler<Revoke>
{
    public override Task Handle(PublishedEvent<Revoke> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Authorization revoked: {@event.Message.ClientId} {@event.Message.UserLogin}");

        return Task.CompletedTask;
    }
}