using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Api.Requests;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.Models;

namespace twitchstreambot.Api;

public class TwitchHelix(HttpClient client, IServiceProvider provider)
{
    public HttpClient Client => client;

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public async Task<HelixCollectionResponse<TwitchUser>?> GetUsersByName(params string[] names)
    {
        var parameters = names.AsQueryParameter("login");

        var response = await client.GetAsync($"helix/users?{parameters}");

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<HelixCollectionResponse<TwitchUser>>(content, _options);
    }

    public TRequest? GetRequest<TRequest>()
        where TRequest : HelixRequest
    {
        return provider.GetService(typeof(TRequest)) as TRequest;
    }
}