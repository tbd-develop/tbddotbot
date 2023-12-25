using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using twitchstreambot.Models;

namespace twitchstreambot.Api
{
    public class TwitchApi(HttpClient client)
    {
        public async Task<ValidationResponse?> Validate(string authToken)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{client.BaseAddress}oauth2/validate"),
                Method = HttpMethod.Get,
                Headers = { { "Authorization", $"OAuth {authToken}" } }
            };

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) return default;

            return
                JsonSerializer.Deserialize<ValidationResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}