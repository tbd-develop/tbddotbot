using System;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Parsing;

namespace twitchbot.Commands;

[TwitchCommand("points")]
public class PointsCommand : ITwitchCommand
{
    public const double PointsAwarded = 63552360298662;

    public bool CanExecute(TwitchMessage message) => true;

    public string Execute(TwitchMessage message)
    {
        if (message.User is null)
        {
            return string.Empty;
        }

        return !message.User.Name.Equals("DFluxStreams", StringComparison.CurrentCultureIgnoreCase)
            ? $"You have {PointsAwarded} points!"
            : $"Hey @DFluxStreams, you have {PointsAwarded} points. Oh, no, sorry, this just in. You don't! ;)";
    }
}