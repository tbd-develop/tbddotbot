using System;
using System.ComponentModel;

namespace twitchbot.commands.TwitchMath
{
    public abstract class TwitchMath<T> : TwitchMathBase
        where T : IConvertible
    {
        public override object Add(object left, object right)
        {
            return Add((T)left, (T)right);
        }

        public override object Subtract(object left, object right)
        {
            return Subtract((T)left, (T)right);
        }

        public override object Multiply(object left, object right)
        {
            return Multiply((T)left, (T)right);
        }

        public override object Divide(object left, object right)
        {
            return Divide((T)left, (T)right);
        }

        public abstract object Add(T left, T right);
        public abstract object Subtract(T left, T right);
        public abstract object Multiply(T left, T right);
        public abstract object Divide(T left, T right);
    }
}