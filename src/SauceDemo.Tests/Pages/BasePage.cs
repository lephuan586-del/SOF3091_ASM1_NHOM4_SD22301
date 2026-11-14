using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver, WebDriverWait wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public IWebElement WaitVisible(By locator)
        {
            return Wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            })!;
        }

        public void Click(By locator)
        {
            var element = WaitVisible(locator);
            element.Click();
        }

        public void Type(By locator, string text)
        {
            var element = WaitVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public string GetText(By locator)
        {
            return WaitVisible(locator).Text;
        }

        public bool IsDisplayed(By locator)
        {
            try
            {
                return WaitVisible(locator).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public string CurrentUrl => Driver.Url;
    }
}
