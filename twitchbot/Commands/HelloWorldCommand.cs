using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchbot.Commands;

[TwitchCommand("hello")]
public class HelloWorldCommand : ITwitchCommand
{
    public bool CanExecute(TwitchMessage message) => true;

    public string Execute(TwitchMessage message)
    {
        return "Hello, world!";
    }
}