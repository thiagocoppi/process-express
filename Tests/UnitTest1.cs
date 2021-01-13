using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Tests.Base;

namespace Tests
{
    public class Tests : BaseTestIntegration
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using (var scope = StartupTest().CreateScope())
            {
                Assert.Pass();
            }
        }
    }
}