using System;
using FluentAssertions;
using NSubstitute;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure;
using Xunit;

namespace twitchstreambot.tests.ConcerningDispatch;

public class when_invalid_message
{
    private DefaultMessageDispatcher Subject = null!;
    private ICommandLookup CommandLookup;
    private IServiceProvider ServiceProvider;
    private MessageResult Result = null!;

    public when_invalid_message()
    {
        Arrange();

        Act();
    }

    [Fact]
    public void no_response_is_returned()
    {
        Result.IsResponse.Should().BeFalse();
        Result.WasParsed.Should().BeFalse();
    }

    private void Arrange()
    {
        CommandLookup = Substitute.For<ICommandLookup>();
        ServiceProvider = Substitute.For<IServiceProvider>();

        Subject = new DefaultMessageDispatcher(CommandLookup, ServiceProvider);
    }

    private void Act()
    {
        Result = Subject.Dispatch("fail message");
    }
}