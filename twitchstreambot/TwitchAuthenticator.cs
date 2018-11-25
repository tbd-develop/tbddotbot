using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using twitchstreambot.models;

namespace twitchstreambot
{
    public class TwitchAuthenticator
    {
        private readonly string _authFilePath;
        public string ClientIdentifier { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string AuthenticationToken { get; private set; }

        public TwitchAuthenticator(string authFilePath)
        {
            _authFilePath = authFilePath;
            IsAuthenticated = false;
            AuthenticationToken = string.Empty;
        }

        public bool Authenticate()
        {
            bool result = true;
            Auth details;

            using (StreamReader reader = new StreamReader(_authFilePath))
            {
                details = JsonConvert.DeserializeObject<Auth>(reader.ReadToEnd());
            }

//            if (details.Expiration == null || details.Expiration < DateTime.UtcNow)
//            {
//                result = RefreshAuthToken(ref details);
//
//                if (result)
//                {
//                    IsAuthenticated = true;
//                    AuthenticationToken = details.AuthToken;
//                    
//                    SaveAuthentication(details);
//                }
//            }
//            else
//            {
            IsAuthenticated = true;
            AuthenticationToken = details.AuthToken;
            ClientIdentifier = details.ClientId;
//            }

            return result;
        }

        private bool RefreshAuthToken(ref Auth details)
        {
            using (HttpClient client = new HttpClient {BaseAddress = new Uri("https://id.twitch.tv/oauth2/")})
            {
                string requestUrl =
                    $"token?client_id={details.ClientId}&client_secret={details.Secret}&grant_type=client_credentials&scope={Uri.EscapeDataString(details.Scope)}";

                var response = client.PostAsync(requestUrl, null).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var authenticationResponse =
                    JsonConvert.DeserializeObject<TwitchTokenResponse>(response.Content.ReadAsStringAsync().Result);

                details.AuthToken = authenticationResponse.AccessToken;
                details.Expiration = DateTime.UtcNow.AddSeconds(authenticationResponse.ExpiresIn);
            }

            return true;
        }

        private void SaveAuthentication(Auth details)
        {
            using (StreamWriter writer = new StreamWriter(_authFilePath, false))
            {
                writer.Write(JsonConvert.SerializeObject(details));
            }
        }
    }
}