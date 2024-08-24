using System.Threading;
using System.Threading.Tasks;
using twitchbot.Infrastructure;
using twitchstreambot.Infrastructure;
using twitchstreambot.Parsing;

namespace twitchbot.Middleware;

public class GameMiddleware(
    IStreamOutput streamOutput, 
    GameState state) : IMessagingMiddleware
{
    public ValueTask<MessageResult> Execute(MessagingContext context, CancellationToken cancellationToken = default)
    {
        if (context.Message.MessageType != IRCMessageType.PrivateMessage)
        {
            return new ValueTask<MessageResult>(MessageResult.NoAction());
        }

        if (!state.IsStarted ||
            state.CurrentPlayer != context.Message.User.Name)
            return new ValueTask<MessageResult>(MessageResult.NoAction());

        if (context.Message.IsBotCommand &&
            context.Message.Command.Action == "gamestart")
        {
            streamOutput.SendToStream(state.GetRoomDescription());
        }

        if (context.Message.Content.StartsWith("#"))
        {
            var response = state.ProcessCommand(context.Message.Content.Substring(1));

            if (response is not null)
            {
                streamOutput.SendToStream(response);
            }
        }

        return new ValueTask<MessageResult>(MessageResult.NoAction());
    }
}