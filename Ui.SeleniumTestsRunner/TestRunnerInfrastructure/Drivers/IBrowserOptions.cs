using OpenQA.Selenium;
using Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Config;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Drivers
{
    public interface IBrowserOptions
    {
        ISettings Settings { get; set; }

        DriverOptions GetOptions();
    }
}