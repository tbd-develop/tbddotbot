using System;

namespace twitchbot.commands.TwitchMath
{
    public class TwitchDoubleMath : TwitchMath<double>
    {
        public override bool CanConvert(string left, string right)
        {
            if (left.Contains(".") || right.Contains("."))
            {
                double aleft, aright;

                if (double.TryParse(left, out aleft) &&
                    double.TryParse(right, out aright))
                {
                    return true;
                }
            }

            return false;
        }

        protected override object Convert(string input)
        {
            double answer;

            double.TryParse(input, out answer);

            return answer;
        }

        public override object Add(double left, double right)
        {
            return left + right;
        }

        public override object Subtract(double left, double right)
        {
            return left - right;
        }

        public override object Multiply(double left, double right)
        {
            return left * right;
        }

        public override object Divide(double left, double right)
        {
            if (Math.Abs(right) < 0.0)
            {
                return 0;
            }

            return left / right;
        }
    }
}