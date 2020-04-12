using OpenQA.Selenium;
using Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Config;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Drivers
{
    public class EdgeOptions : IBrowserOptions
    {
        public ISettings Settings { get; set; }

        public DriverOptions GetOptions()
        {
            return new OpenQA.Selenium.Edge.EdgeOptions();
        }
    }
}
