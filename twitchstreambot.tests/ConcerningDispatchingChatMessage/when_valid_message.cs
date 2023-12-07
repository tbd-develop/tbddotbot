using System;
using System.Threading;
using FluentAssertions;
using NSubstitute;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure;
using Xunit;

namespace twitchstreambot.tests.ConcerningDispatch;

public class when_valid_message
{
    private const string SampleValidMessage = """
                                              @badge-info=subscriber/54;badges=broadcaster/1,subscriber/0,turbo/1;client-nonce=d5624b7657cf2a08a8f5a870a9859ea5;color=#FF0000;display-name=tbdgamer;emotes=;
                                              first-msg=0;flags=;id=238c91a7-e4f4-4e6e-a83d-1a7c2ccec7a6;mod=0;returning-chatter=0;room-id=51497560;subscriber=1;tmi-sent-ts=1700946316341;turbo=1;user-id=5
                                              1497560;user-type= :tbdgamer!tbdgamer@tbdgamer.tmi.twitch.tv PRIVMSG #tbdgamer :Not A Command
                                              """;

    private DefaultMessageDispatcher Subject = null!;
    private IMessagingPipeline MessagingPipeline = null!;
    private MessageResult Result;

    public when_valid_message()
    {
        Arrange();

        Act();
    }

    [Fact]
    public void no_response_is_returned()
    {
        MessagingPipeline
            .Received()
            .Dispatch(Arg.Is<MessagingContext>(x =>
                !x.Message.IsBotCommand && x.Message.Content == "Not A Command"), Arg.Any<CancellationToken>());
    }

    private void Arrange()
    {
        MessagingPipeline = Substitute.For<IMessagingPipeline>();

        Subject = new DefaultMessageDispatcher(MessagingPipeline);
    }

    private void Act()
    {
        Subject.Dispatch(SampleValidMessage)
            .GetAwaiter()
            .GetResult();
    }
}