using System;
using System.Threading.Tasks;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using NUnit.Framework;

namespace Load.Test
{
    [TestFixture]
    public class LoadTestExample
    {
        private IStep _step;
        
        [SetUp]
        public void SetUp()
        {
            _step = HttpStep.Create("simple step", (context) =>
                Http.CreateRequest("GET", "https://gitter.im")
                    .WithHeader("Accept", "text/html")
                    .WithCheck(response => Task.FromResult(response.IsSuccessStatusCode))
            );
        }

        [Test]
        public void TestMethod1()
        {
            var scenario = ScenarioBuilder.CreateScenario("test gitter", new[] { _step })
                .WithConcurrentCopies(1000)
                .WithDuration(TimeSpan.FromSeconds(15));
            NBomberRunner.RegisterScenarios(scenario).RunTest();
            
        }
    }
}
