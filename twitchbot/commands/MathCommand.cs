using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using twitchbot.commands.TwitchMath;
using twitchstreambot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("math")]
    public class MathCommand : ITwitchCommand
    {
        private string _usageString = "usage: Math x +-/* y i.e. 4+4, 2*4";
        public bool CanExecute(IDictionary<string, string> headers)
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            string pattern = @"^\s*(?<left>[\d.]*)\s*(?<operator>[+-\/*]?)\s*(?<right>[\d.]*)";

            var value = Regex.Match(string.Join(' ', args), pattern);

            if (value.Success)
            {
                string left = value.Groups["left"].Value;
                string right = value.Groups["right"].Value;
                string @operator = value.Groups["operator"].Value;

                var math = TwitchMathBase.GetMath(left, right);

                return $"The answer is {math.GetResult(left, right, @operator)}";
            }

            return _usageString;
        }


        public string DoTheMath(long left, long right, string @operator)
        {
            if (left <= 0 && right <= 0)
            {
                return "Really, I have my limits!";
            }

            switch (@operator)
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
                        if (right == 0)
                        {
                            return $"Try it again punk!";
                        }

                        return $"the answer is ... {(float)left / right}";
                    }
                case "*":
                    {
                        return $"the answer is ... {left * right}";
                    }
            }

            return "Huh?";
        }

    }
}