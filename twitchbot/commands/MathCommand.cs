using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using twitchbot.commands.TwitchMath;
using twitchstreambot.infrastructure;

namespace twitchbot.commands
{
    [TwitchCommand("math")]
    public class MathCommand : ITwitchCommand
    {
        private string _usageString = "usage: Math x +-/* y i.e. 4+4, 2*4";
        public bool CanExecute()
        {
            return true;
        }

        public string Execute(params string[] args)
        {
            string pattern = @"^\s*(?<left>[\d.]*)\s*(?<operator>[+-\/*]?)\s*(?<right>[\d.]*)";

            var value = Regex.Match(string.Join(' ', args), pattern);

            if (value.Success)
            {
                string left = value.Groups["left"].Value.TrimToXChars(12);
                string right = value.Groups["right"].Value.TrimToXChars(12);
                string @operator = value.Groups["operator"].Value;

                var math = TwitchMathBase.GetMath(left, right);

                return $"The answer; {left}{@operator}{right} = {math.GetResult(left, right, @operator)}";
            }

            return _usageString;
        }
    }

    public static class UnnecessaryExtensions
    {
        public static string TrimToXChars(this string input, int chars)
        {
            return input.Substring(0, input.Length > chars ? chars : input.Length);
        }
    }
}