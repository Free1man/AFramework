﻿using OpenQA.Selenium;
using Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Config;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Drivers
{
    public class InternetExplorerOptions : IBrowserOptions
    {
        public ISettings Settings { get; set; }

        public DriverOptions GetOptions()
        {
            var options = new OpenQA.Selenium.IE.InternetExplorerOptions();
            options.IgnoreZoomLevel = true;
            return options;
        }
    }
}
