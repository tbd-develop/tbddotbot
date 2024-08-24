using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using twitchstreambot.Infrastructure.Delegates;
using twitchstreambot.Models;

namespace twitchstreambot.Api
{
    public class TwitchApi(
        HttpClient client,
        CreateTwitchApiOptionsDelegate options,
        IConfiguration configuration)
    {
        private readonly string _authToken = configuration["Twitch:authToken"];

        private readonly (string identifier, string secret)? _clientCredentials =
            new Lazy<(string, string)>(() => (configuration["Twitch:clientId"]!, configuration["Twitch:clientSecret"]!))
                .Value;

        public async Task<ValidationResponse?> Validate()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{client.BaseAddress}oauth2/validate"),
                Method = HttpMethod.Get,
                Headers = { { "Authorization", $"OAuth {_authToken}" } }
            };

            var response = await client.SendAsync(request);

            return !response.IsSuccessStatusCode
                ? default
                : JsonSerializer.Deserialize<ValidationResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<TwitchTokenResponse?> AuthorizeClientCredentials()
        {
            (string clientIdentifier, string clientSecret) = _clientCredentials!.Value;

            string url =
                $"oauth2/token?client_id={clientIdentifier}&client_secret={clientSecret}&grant_type=client_credentials";

            var response = await client.PostAsync(url, null);

            return !response.IsSuccessStatusCode
                ? default
                : JsonSerializer.Deserialize<TwitchTokenResponse>(await response.Content.ReadAsStringAsync(),
                    options());
        }

        public string GenerateAuthorizeCodeGrantFlowUrl(string[] scopes)
        {
            var (clientIdentifier, _) = _clientCredentials!.Value;

            if (client.BaseAddress is null)
            {
                return string.Empty;
            }

            var baseAddress = client.BaseAddress!.ToString();
            var encodedScopes = string.Join("%20", scopes);
            var redirectOnGrantUrl = configuration["Twitch:redirectOnGrantUrl"];

            var url =
                $"{baseAddress}/oauth2/authorize?response_type=code&client_id={clientIdentifier}&redirect_uri={redirectOnGrantUrl}&scope={encodedScopes}";

            return url;
        }
    }
}