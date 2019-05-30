using System;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace twitchbot.commands.TwitchMath
{
    public abstract class TwitchMathBase
    {
        public abstract bool CanConvert(string left, string right);
        protected abstract object Convert(string input);
        public abstract object Add(object left, object right);
        public abstract object Subtract(object left, object right);
        public abstract object Multiply(object left, object right);
        public abstract object Divide(object left, object right);

        public object GetResult(object left, object right, string @operator)
        {
            try
            {
                if (left is string lv && right is string rv)
                {
                    switch (@operator)
                    {
                        case "+":
                            return Add(Convert(lv), Convert(rv));
                        case "-":
                            return Subtract(Convert(lv), Convert(rv));
                        case "/":
                            return Divide(Convert(lv), Convert(rv));
                        case "*":
                            return Multiply(Convert(lv), Convert(rv));
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"What the {exception}");
            }

            return 0;
        }

        public static TwitchMathBase GetMath(string left, string right)
        {
            return new TwitchDoubleMath();
        }
    }
}