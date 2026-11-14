using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Tests.Config;

namespace SauceDemo.Tests.Base
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected WebDriverWait Wait { get; private set; } = null!;
        protected TestSettings Settings { get; private set; } = null!;

        [SetUp]
        public virtual void SetUp()
        {
            Settings = ConfigReader.Settings;

            var options = new ChromeOptions();
            if (Settings.Headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
            }
            options.AddArgument("--window-size=1920,1080");

            Driver = new ChromeDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Settings.TimeoutSeconds));
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }
}
