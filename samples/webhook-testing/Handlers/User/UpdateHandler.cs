using twitchstreambot.webhooks.Events.User;
using twitchstreambot.webhooks.Infrastructure;

namespace webhook_testing.Handlers.User;

public class UpdateHandler : TwitchWebhookEventHandler<Update>
{
    public override Task Handle(PublishedEvent<Update> @event, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"User updated: {@event.Message.UserId} {@event.Message.UserName} {@event.Message.UserLogin}");

        return Task.CompletedTask;
    }
}