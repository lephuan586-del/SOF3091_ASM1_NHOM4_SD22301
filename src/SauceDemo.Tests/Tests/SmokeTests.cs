using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class SmokeTests
    {
        private IWebDriver _driver = null!;
        private WebDriverWait _wait = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            var headlessEnv = Environment.GetEnvironmentVariable("TestSettings__Headless");
            if (string.Equals(headlessEnv, "true", StringComparison.OrdinalIgnoreCase))
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
            }
            options.AddArgument("--window-size=1920,1080");

            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test_TrangChu_TieuDeChinhXac()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _wait.Until(d => !string.IsNullOrEmpty(d.Title));

            Assert.That(_driver.Title, Is.EqualTo("Swag Labs"));
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}
