using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.infrastructure.DependencyInjection;

namespace twitchstreambot.tests.ConcerningDI
{
    [TestFixture]
    public class when_constructing_an_object_with_a_dependency
    {
        private Container Subject;
        private TestClass Result;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Subject = new Container();
            Subject.When<TestClass>().Use<TestClass>();
            Subject.When<TestDependency>().Use<TestDependency>();

            Result = Subject.GetInstance<TestClass>();
        }

        [Test]
        public void instance_is_returned()
        {
            Result.Should().NotBeNull();
        }

        [Test]
        public void instance_has_dependency_attached()
        {
            Result.Dependency.Should().NotBeNull();
        }

        public class TestClass
        {
            public TestDependency Dependency { get; set; }

            public TestClass(TestDependency dependency)
            {
                Dependency = dependency;
            }
        }

        public class TestDependency
        {

        }
    }
}