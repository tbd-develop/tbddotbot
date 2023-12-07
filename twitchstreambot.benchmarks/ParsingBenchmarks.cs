using BenchmarkDotNet.Attributes;
using twitchstreambot.Parsing;

namespace twitchstreambot.benchmarks;

[MemoryDiagnoser, MinColumn, MaxColumn, IterationsColumn, RankColumn, MedianColumn]
public class ParsingBenchmarks
{
    public const string SimpleUserStateCommand =
        "@badge-info=;badges=moderator/1;color=;display-name=tbdDOTbot;emote-sets=0,300374282,537206155,610186276;id=6027164f-376d-43dc-b706-86b31b411795;mod=1;subscriber=0;user-type=mod :tmi.twitch.tv USERSTATE #tbdgamer";

    public const string SimpleJoinCommand =
        ":anotherttvviewer!anotherttvviewer@anotherttvviewer.tmi.twitch.tv JOIN #tbdgamer";

    public const string SimpleMessageCommand =
        "@badge-info=subscriber/54;badges=broadcaster/1,subscriber/0,turbo/1;client-nonce=5b1a981c25ee301c41ed400bc347b292;color=#FF0000;display-name=tbdgamer;emotes=;\n    first-msg=0;flags=;id=8ddb4688-979f-40d4-b3a8-b1e29751a692;mod=0;returning-chatter=0;room-id=51497560;subscriber=1;tmi-sent-ts=1700946274598;turbo=1;user-id=51497560;user-type= :tbdgamer!tbdgamer@tbdgamer.tmi.twitch.tv PRIVMSG #tbdgamer :This is a world of messages";

    public const string MaximumLengthCommand = """
                                               @badge-info=subscriber/54;badges=broadcaster/1,subscriber/0,turbo/1;client-nonce=d5624b7657cf2a08a8f5a870a9859ea5;color=#FF0000;display-name=tbdgamer;emotes=;
                                               first-msg=0;flags=;id=238c91a7-e4f4-4e6e-a83d-1a7c2ccec7a6;mod=0;returning-chatter=0;room-id=51497560;subscriber=1;tmi-sent-ts=1700946316341;turbo=1;user-id=5
                                               1497560;user-type= :tbdgamer!tbdgamer@tbdgamer.tmi.twitch.tv PRIVMSG #tbdgamer :Hey there, Ciubix8513! As an AI, I do not condone or support violence towards
                                               animals. Instead of biting your cat back, I recommend using positive reinforcement and redirecting her behavior with toys or treats. Cats can sometimes bite a
                                               s a form of play or communication, so understanding her body language and providing her with appropriate outlets can help. Remember, no cat biting back battles-let's keep it peaceful in the feline kingdom! the quick brown fox jumped over the lazy dog the quick bro
                                               """;

    [Benchmark]
    public void ParseSimpleCommand()
    {
        TwitchCommandParser.TryMatch(SimpleMessageCommand, out _);
    }

    [Benchmark]
    public void ParseMaximumLengthCommand()
    {
        TwitchCommandParser.TryMatch(MaximumLengthCommand, out _);
    }

    [Benchmark]
    public void ParseSimpleJoinCommand()
    {
        TwitchCommandParser.TryMatch(SimpleJoinCommand, out _);
    }

    [Benchmark]
    public void ParseSimpleUserStateCommand()
    {
        TwitchCommandParser.TryMatch(SimpleUserStateCommand, out _);
    }
}