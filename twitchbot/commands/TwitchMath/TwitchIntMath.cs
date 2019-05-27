using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace twitchbot.commands.TwitchMath
{
    public class TwitchIntMath : TwitchMath<int>
    {
        public override bool CanConvert(string left, string right)
        {
            int lefta, righta;

            if (Int32.TryParse(left, out lefta) && Int32.TryParse(right, out righta))
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

        public override object Add(int left, int right)
        {
            return left + right;
        }

        public override object Subtract(int left, int right)
        {
            return left - right;
        }

        public override object Multiply(int left, int right)
        {
            return left * right;
        }

        public override object Divide(int left, int right)
        {
            if (right == 0)
            {
                return 0;
            }

            return left / right;
        }
    }
}