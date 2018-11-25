using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using twitchbot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("roll")]
    public class DiceRollerCommand : ITwitchCommand
    {
        public string Execute(params string[] args)
        {
            var seed = new Random();
            
            if (!args.Any())
            {
                return $"{FlipCoin(seed)}";
            }

            string pattern = @"(?<number>[\d]{1})W(?<sides>[\d]{1,2})";

            var value = Regex.Match(args[0], pattern);

            if (value.Success)
            {
                int numberOfDice = Int32.Parse(value.Groups["number"].Value);
                int numberOfSides = Int32.Parse(value.Groups["sides"].Value);

                if (numberOfSides == 0)
                {
                    return "Really, what would you like me to do with a 0 sided die?";
                }

                List<string> results = new List<string>();

                for (int die = 0; die < numberOfDice; die++)
                {
                    if (numberOfSides == 2)
                    {
                        results.Add($"[{FlipCoin(seed)}]");
                    }
                    else
                    {
                        int roll = (seed.Next() % numberOfSides);

                        results.Add($"[{die + 1}: {roll + 1}]");
                    }
                }

                return string.Join(", ", results.ToArray());
            }

            return "Roll what??!";
        }

        private string FlipCoin(Random rnd)
        {
            int result = rnd.Next() % 2;

            return result == 1 ? "Heads" : "Tails";
        }
    }
}