using twitchstreambot.webhooks.Events.User.Authorization;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.User;

public class AuthorizationGrantHandler : TwitchWebhookEventHandler<Grant>
{
    public override Task Handle(PublishedEvent<Grant> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Authorization granted: {@event.Message.ClientId} {@event.Message.UserLogin}");

        return Task.CompletedTask;
    }
}