using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace twitchbot.commands.TwitchMath
{
    public class TwitchIntMath : TwitchMath<long>
    {
        public override bool CanConvert(string left, string right)
        {
            long lefta, righta;

            if (long.TryParse(left, out lefta) && long.TryParse(right, out righta))
            {
                return true;
            }

            return false;
        }

        protected override object Convert(string input)
        {
            int answer;

            int.TryParse(input, out answer);

            return answer;
        }

        public override object Add(long left, long right)
        {
            return left + right;
        }

        public override object Subtract(long left, long right)
        {
            return left - right;
        }

        public override object Multiply(long left, long right)
        {
            return left * right;
        }

        public override object Divide(long left, long right)
        {
            if (right == 0)
            {
                return 0;
            }

            return left / right;
        }
    }
}