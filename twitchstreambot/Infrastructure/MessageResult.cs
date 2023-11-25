namespace twitchstreambot.Infrastructure;

public class MessageResult
{
    public static MessageResult NoResponse() => new() { IsResponse = false };
    public static MessageResult Respond(string message) => new(message) { IsResponse = true };

    private MessageResult(string? content = default)
    {
        Content = content;
    }

    public bool IsResponse { get; set; }
    public string? Content { get; private set; }
}