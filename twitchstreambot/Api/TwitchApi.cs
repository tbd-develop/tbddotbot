using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using twitchstreambot.Models;

namespace twitchstreambot.Api
{
    public class TwitchApi(HttpClient client)
    {
        private readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

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

        public async Task<TwitchTokenResponse?> AuthorizeClientCredentials(string clientIdentifier,
            string clientSecret)
        {
            string url =
                $"oauth2/token?client_id={clientIdentifier}&client_secret={clientSecret}&grant_type=client_credentials";

            var response = await client.PostAsync(url, null);

            return !response.IsSuccessStatusCode
                ? default
                : JsonSerializer.Deserialize<TwitchTokenResponse>(await response.Content.ReadAsStringAsync(), Options);
        }
    }
}