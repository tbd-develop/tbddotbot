using System;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using twitchbot.models;

namespace twitchbot
{
    class Program
    {
        public class TwitchStreamBot
        {
            private readonly TwitchConnection _connection;
            private readonly Auth _authentication;

            public string Error { get; private set; }
            
            public TwitchStreamBot(TwitchConnection connection, Auth authentication)
            {
                _connection = connection;
                _authentication = authentication;
            }

            public int Start()
            {
                using (var client = new TcpClient(_connection.HostName, _connection.Port))
                {
                    using (var stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    using (StreamWriter writer = new StreamWriter(stream) {AutoFlush = true})
                    {
                        Authenticate(writer);

                        if (!VerifyConnected(reader))
                        {
                            return -1;
                        }

                        ListenForMessages(reader, writer);
                    }
                }

                return 0;
            }

            private void Authenticate(StreamWriter writer)
            {
                writer.WriteLine($"PASS oauth:{_authentication.AuthToken}");
                writer.WriteLine($"NICK {_connection.BotName}");
                writer.WriteLine($"JOIN #{_connection.Channel}");
            }

            private bool VerifyConnected(StreamReader reader)
            {
                bool result = false;
                string inputBuffer = string.Empty;
                string verifyRegex =
                    @":(?<address>.*)\s(?<code>[\d]*)\s(?<botname>.*)\s#(?<channel>.*)\s:End of \/NAMES list";

                try
                {
                    while ((inputBuffer = reader.ReadLine()) != null)
                    {
                        var match = Regex.Match(inputBuffer, verifyRegex);

                        if (match.Success)
                        {
                            if (match.Groups["code"].Value == "366")
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                catch (TimeoutException exception)
                {
                    Error = exception.Message;
                }

                return result;
            }

            private void ListenForMessages(StreamReader reader, StreamWriter writer)
            {
                bool result = false;
                string inputBuffer = string.Empty;
                string verifyRegex =
                    @":(?<address>.*)\s(?<code>[\d]*)\s(?<botname>.*)\s#(?<channel>.*)\s:End of \/NAMES list";

                try
                {
                    while ((inputBuffer = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(inputBuffer);
                    }
                }
                catch (TimeoutException exception)
                {
                    Error = exception.Message;
                }
            }
            
            public class TwitchConnection
            {
                public string HostName { get; set; }
                public int Port { get; set; }
                public string Channel { get; set; }
                public string BotName { get; set; }
            }
        }

        static void Main(string[] args)
        {
            Auth authentication;

            using (StreamReader reader = new StreamReader("auth.json"))
            {
                authentication = JsonConvert.DeserializeObject<Auth>(reader.ReadToEnd());
            }

            TwitchStreamBot bot = new TwitchStreamBot(new TwitchStreamBot.TwitchConnection()
            {
                BotName = "tbddotbot",
                HostName = "irc.chat.twitch.tv",
                Channel = "tbdgamer",
                Port = 6667
            }, authentication);

            if (bot.Start() != 0)
            {
                Console.WriteLine("Error Status");
                Console.WriteLine(bot.Error);
            }
        }

        public static TwitchData GetStreamStatistics(string streamName, Auth authentication)
        {
            string queryUrl = "https://api.twitch.tv/kraken/streams/";

            using (HttpClient client = new HttpClient {BaseAddress = new Uri("https://api.twitch.tv/kraken/")})
            {
                var response = client.GetAsync($"streams/{streamName}?client_id={authentication.ClientId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<TwitchData>(responseData);
                }
            }

            return null;
        }
    }
}