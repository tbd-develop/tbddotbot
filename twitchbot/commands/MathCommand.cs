using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using twitchstreambot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("math")]
    public class MathCommand : ITwitchCommand
    {
        public bool CanExecute(IDictionary<string, string> headers)
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            string pattern = @"(?<left>[\d]*)(?<operator>[+-\/*]?)(?<right>[\d]*)";

            var value = Regex.Match(args[0], pattern);

            if (value.Success)
            {
                long left = GetValue(value.Groups["left"].Value);
                long right = GetValue(value.Groups["right"].Value);

                if (left <= 0 && right <= 0)
                {
                    return "Really, I have my limits!";
                }

                switch (value.Groups["operator"].Value)
                {
                    case "+":
                        {
                            return $"the answer is ... {left + right}";
                        }
                    case "-":
                        {
                            return $"the answer is ... {left - right}";
                        }
                    case "/":
                        {
                            return $"the answer is ... {left / right}";
                        }
                    case "*":
                        {
                            return $"the answer is ... {left * right}";
                        }
                }
            }

            return "Unfortunately, I can't finish";
        }

        private long GetValue(string value)
        {
            long result;

            if (long.TryParse(value, out result))
            {
                return result;
            }

            return 0;
        }
    }
}