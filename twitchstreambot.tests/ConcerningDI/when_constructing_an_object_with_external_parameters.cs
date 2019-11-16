using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using twitchstreambot.Infrastructure.DependencyInjection;

namespace twitchstreambot.tests.ConcerningDI
{
    [TestFixture]
    public class when_constructing_an_object_with_external_parameters
    {
        private Container Subject;
        private TestClass Result;

        private Dictionary<string, string> Headers = new Dictionary<string, string> { { "Key", "Value" } };

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Subject = new Container();
            Subject.When<TestClass>().Use<TestClass>();
            Subject.When<TestDependency>().Use<TestDependency>();

            Result = Subject.GetInstance<TestClass>(new object[] { Headers });
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

        [Test]
        public void instance_has_external_parameters_attached()
        {
            Result.Headers.Should().NotBeNull();
            Result.Headers.ContainsKey("Key");
        }

        public class TestClass
        {
            public Dictionary<string, string> Headers { get; }
            public TestDependency Dependency { get; set; }

            public TestClass(TestDependency dependency, Dictionary<string, string> headers)
            {
                Headers = headers;
                Dependency = dependency;
            }
        }

        public class TestDependency
        {

        }
    }
}