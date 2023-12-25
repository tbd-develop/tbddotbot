using System.Threading.Tasks;

namespace twitchstreambot.Api.Requests;

public abstract class HelixRequest(TwitchHelix helix)
{}

public abstract class HelixRequest<TParameters, TResult>(TwitchHelix helix)
    : HelixRequest(helix)
{
    public abstract Task<TResult?> Execute(TParameters parameters);
}