using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using twitchbot.Infrastructure;
using twitchstreambot.infrastructure;

namespace twitchbot.commands.StreamGames.Hangman
{
    [TwitchCommand("guess")]
    public class GuessCommand : ITwitchCommand
    {
        private readonly Dictionary<string, string> _headers;
        private readonly SignalRClient _client;

        public GuessCommand(Dictionary<string, string> headers, SignalRClient client)
        {
            _headers = headers;
            _client = client;
        }

        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            if (args.Any())
            {
                if (IsValidCharacterGuess(args[0]))
                {
                    Task.Run(async () =>
                    {
                        await _client.Connection.SendCoreAsync("GuessLetter", new object[]
                        {
                            new TwitchChatCommand()
                            {
                                UserName = _headers["display-name"],
                                Value = args[0]
                            }
                        });
                    });
                }
                else if (IsCompleteGuess(args))
                {
                    Task.Run(async () =>
                    {
                        await _client.Connection.SendCoreAsync("GuessWord", new object[]
                        {
                            new TwitchChatCommand()
                            {
                                UserName = _headers["display-name"],
                                Value = string.Join(" ", args)
                            }
                        });
                    });
                }
            }
            else
            {
                return "Invalid Guess!";
            }

            return string.Empty;
        }

        private bool IsValidCharacterGuess(string guess)
        {
            if (!string.IsNullOrEmpty(guess))
            {
                if (guess.Length == 1)
                {
                    if (Regex.IsMatch(guess, @"^[a-zA-Z0-9]{1}"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsCompleteGuess(string[] args)
        {
            string pattern = @"^([\w\d\s!]*)$";
            string complete = string.Join(" ", args);

            return Regex.IsMatch(complete, pattern);
        }

    }
}