using System;
using System.Threading;
using FluentAssertions;
using NSubstitute;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure;
using Xunit;

namespace twitchstreambot.tests.ConcerningDispatch;

public class when_invalid_message
{
    private DefaultMessageDispatcher Subject = null!;
    private IMessagingPipeline MessagingPipeline = null!;

    public when_invalid_message()
    {
        Arrange();

        Act();
    }

    [Fact]
    public void messaging_pipeline_does_not_receive_message_context()
    {
        MessagingPipeline
            .DidNotReceive()
            .Dispatch(Arg.Any<MessagingContext>(), Arg.Any<CancellationToken>());
    }

    private void Arrange()
    {
        MessagingPipeline = Substitute.For<IMessagingPipeline>();

        Subject = new DefaultMessageDispatcher(MessagingPipeline);
    }

    private void Act()
    {
        Subject.Dispatch("fail message", CancellationToken.None)
            .GetAwaiter()
            .GetResult();
    }
}