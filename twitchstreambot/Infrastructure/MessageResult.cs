namespace twitchstreambot.Infrastructure;

public class MessageResult
{
    public static MessageResult NoResponse(bool parsed = false) => new() { IsResponse = false, WasParsed = parsed };
    public static MessageResult Respond(string message) => new(message) { IsResponse = true };

    private MessageResult(string? content = default)
    {
        Content = content;
    }

    public bool IsResponse { get; set; }
    public bool WasParsed { get; set; }
    public string? Content { get; private set; }
}