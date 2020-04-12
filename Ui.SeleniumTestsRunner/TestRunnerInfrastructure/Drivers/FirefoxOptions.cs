using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Config;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Drivers
{
    public class FirefoxOptions : IBrowserOptions
    {
        public ISettings Settings { get; set; }

        public DriverOptions GetOptions()
        {
            var profile = new FirefoxProfile();
            var options = new OpenQA.Selenium.Firefox.FirefoxOptions();
            //options.SetPreference(FirefoxDriver.ProfileCapabilityName, profile.ToBase64String());
            return options;
        }




    }
}
