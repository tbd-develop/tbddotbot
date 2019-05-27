using NUnit.Framework;
using twitchbot.commands.TwitchMath;

namespace twitchbot.tests
{
    [TestFixture]
    public class when_passing_operator_with_decimals
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
        public void and_operator_is_plus_answer_is_correct()
        {
            Assert.AreEqual(Subject.GetResult(Left, Right, "+"), 5f);
        }

        [Test]
        public void and_operator_is_subtract_answer_is_correct()
        {
            Assert.AreEqual(Subject.GetResult(Left, Right, "-"), 0f);
        }

        [Test]
        public void and_operator_is_divide_answer_is_correct()
        {
            Assert.AreEqual(Subject.GetResult(Left, Right, "/"), 1f);
        }

        [Test]
        public void and_operator_is_multiply_answer_is_correct()
        {
            Assert.AreEqual(Subject.GetResult(Left, Right, "*"), 6.25f);
        }
    }
}