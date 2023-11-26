namespace twitchstreambot.Infrastructure;

public class MessageResult
{
    public static MessageResult Error(string message) => new(message) { IsError = true };
    public static MessageResult Success(string? message = default) => new(message);
    public static MessageResult NoAction() => new();

    private MessageResult(string? content = default)
    {
        Content = content;
    }

    public bool IsError { get; set; }
    public string? Content { get; private set; }
}