using System.ComponentModel;
using NUnit.Framework;
using twitchbot.commands.TwitchMath;

namespace twitchbot.tests
{
    [TestFixture]
    public class when_passing_strings_with_decimals
    {
        private string Left = "2.5";
        private string Right = "2.5";
        private TwitchMathBase Subject;

        [OneTimeSetUp]
        public void SetUp()
        {
            Subject = TwitchMathBase.GetMath(Left, Right);
        }

        [Test]
        public void math_is_double()
        {
            Assert.IsTrue(Subject is TwitchDoubleMath);
        }

        [Test]
        public void left_plus_right_is_answer()
        {
            Assert.AreEqual(Subject.Add(Left, Right), 5.0f);
        }

        [Test]
        public void left_multiplied_by_right_is_answer()
        {
            Assert.AreEqual(Subject.Multiply(Left, Right), 6.25f);
        }

        [Test]
        public void left_divided_by_right_is_answer()
        {
            Assert.AreEqual(Subject.Divide(Left, Right), 1f);
        }

        [Test]
        public void left_minus_right_is_answer()
        {
            Assert.AreEqual(Subject.Subtract(Left, Right), 0f);
        }
    }
}