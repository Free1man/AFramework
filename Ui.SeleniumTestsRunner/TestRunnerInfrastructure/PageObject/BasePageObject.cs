using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Ui.SeleniumTestsRunner.TestRunnerInfrastructure.Runner;

namespace Ui.SeleniumTestsRunner.TestRunnerInfrastructure.PageObject
{
    /// <summary>
    ///     Represents a class for base page object.
    /// </summary>
    public abstract class BasePageObject
    {
        ///// <summary>
        ///// Initializes an instance of page object 
        ///// </summary>
        //protected BasePageObject()
        //{
        //    PageFactory.InitElements(Driver, this);
        //}

        /// <summary>
        ///     Current selenium WebDriver instance.
        /// </summary>
        protected IWebDriver Driver => SeleniumRunner.Instance.Browser.Driver;

        /// <summary>
        ///     Current selenium WebDriverWait instance.
        /// </summary>
        protected WebDriverWait Wait => SeleniumRunner.Instance.Browser.Wait;

        protected IWebElement FindElementByXPath(string xPath)
        {
            Wait.Until(driver => driver.FindElement(By.XPath(xPath)).Displayed);
            return Driver.FindElement(By.XPath(xPath));
        }
    }
}