using FluentAssertions;
using twitchstreambot.Parsing.IRCCommands;
using Xunit;

namespace twitchstreambot.tests.ConcerningMessageParsing;

public class when_message_contains_url
{
    private ParsePrivateMessage Subject;
    private string MessageStarter =
        @"@badge-info=;badges=broadcaster/1,premium/1;user-type= :user!user@user.tmi.twitch.tv PRIVMSG #user :";

    private string MessageContent = "Test http://twitch.tv is a url";
    private string MessageToParse;

    public when_message_contains_url()
    {
        MessageToParse = $"{MessageStarter}{MessageContent}";
        Subject = new ParsePrivateMessage();
    }

    [Fact]
    public void message_is_not_treated_as_bot_message()
    {
        var result = Subject.Do(MessageToParse);

        result!.IsBotCommand.Should().BeFalse();
        result!.Command.Should().BeNull();
    }

    [Fact]
    public void message_is_intact()
    {
        var result = Subject.Do(MessageToParse);

        result!.Content.Should().Be(MessageContent);
    }
}