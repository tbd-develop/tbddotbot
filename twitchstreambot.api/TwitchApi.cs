using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using twitchstreambot.api.Models;

namespace twitchstreambot.api
{
    public class TwitchApi
    {
        private HttpClient _client;

        public TwitchApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<ValidationResponse> Validate(string authToken)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_client.BaseAddress}oauth2/validate"),
                Method = HttpMethod.Get,
                Headers = { { "Authorization", $"OAuth {authToken}" } }
            };

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result =
                    JsonConvert.DeserializeObject<ValidationResponse>(await response.Content.ReadAsStringAsync());

                return result;
            }

            return default;
        }
    }
}