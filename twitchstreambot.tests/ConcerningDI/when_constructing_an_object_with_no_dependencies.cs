using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.infrastructure.DependencyInjection;

namespace twitchstreambot.tests.ConcerningDI
{
    [TestFixture]
    public class when_constructing_an_object_with_no_dependencies
    {
        private Container Subject;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Subject = new Container();
            Subject.When<TestClass>().Use<TestClass>();
        }

        [Test]
        public void instance_is_returned()
        {
            Subject.GetInstance<TestClass>().Should().NotBeNull();
        }

        public class TestClass
        {

        }
    }
}