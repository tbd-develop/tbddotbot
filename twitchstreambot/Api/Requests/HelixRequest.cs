using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace twitchstreambot.Api.Requests;

public delegate void OnConfiguringDelegate(HttpClient client);

public abstract class HelixRequest(TwitchHelix helix);

public abstract class HelixRequest<TResult>(TwitchHelix helix)
    : HelixRequest(helix)
{
    public abstract Task<TResult?> Execute();
    public event OnConfiguringDelegate? OnConfiguring;

    protected void Configure(HttpClient client)
    {
        OnConfiguring?.Invoke(client);
    }
}

public abstract class HelixRequest<TParameters, TResult>(TwitchHelix helix)
    : HelixRequest(helix)
{
    public abstract Task<TResult?> Execute(TParameters parameters);
    
    public event OnConfiguringDelegate? OnConfiguring;

    protected void Configure(HttpClient client)
    {
        OnConfiguring?.Invoke(client);
    }
}