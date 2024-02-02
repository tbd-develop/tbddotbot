using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Delegates;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.Models;

namespace twitchstreambot.Api.Requests;

public class GetChannelsForUsersRequest(TwitchHelix helix, CreateTwitchApiOptionsDelegate options)
    : HelixRequest<string[], HelixCollectionResponse<ChannelResponse>>(helix)
{
    public override async Task<HelixCollectionResponse<ChannelResponse>?> Execute(string[] names)
    {
        var users = await helix.GetUsersByName(names);

        if (users is null || !users.HasData)
        {
            return null;
        }

        var broadcasterIds = users.Data.Select(d => d.Id)
            .AsQueryParameter("broadcaster_id");

        var response = await helix.Client.GetAsync($"helix/channels?{broadcasterIds}");

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<HelixCollectionResponse<ChannelResponse>>(
            content, options());

        return result;
    }
}