using OpenQA.Selenium;
using Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Config;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Drivers
{
    public class ChromeOptions : IBrowserOptions
    {

        public ISettings Settings { get; set; }

        public DriverOptions GetOptions()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            return options;
        }


    }
}
