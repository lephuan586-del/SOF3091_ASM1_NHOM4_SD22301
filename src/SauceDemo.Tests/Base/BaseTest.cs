using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Tests.Config;
using SauceDemo.Tests.Utils;

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
            TestContext.Progress.WriteLine($"[{DateTime.Now:HH:mm:ss}] START {TestContext.CurrentContext.Test.Name} (thread {Environment.CurrentManagedThreadId})");

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
                var outcome = TestContext.CurrentContext.Result.Outcome.Status;
                var statusLabel = outcome == TestStatus.Passed ? "PASS" : "FAIL";
                TestContext.Progress.WriteLine($"[{DateTime.Now:HH:mm:ss}] {statusLabel} {TestContext.CurrentContext.Test.Name} (thread {Environment.CurrentManagedThreadId})");

                if (outcome == TestStatus.Failed && Driver != null)
                {
                    var screenshotPath = ScreenshotHelper.CaptureScreenshot(Driver, TestContext.CurrentContext.Test.Name);
                    if (!string.IsNullOrEmpty(screenshotPath) && File.Exists(screenshotPath))
                    {
                        TestContext.AddTestAttachment(screenshotPath, "Screenshot chup khi test that bai");
                        TestContext.Progress.WriteLine($"[SCREENSHOT] Da luu anh man hinh tai: {screenshotPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"Loi trong TearDown: {ex.Message}");
            }
            finally
            {
                try
                {
                    Driver?.Quit();
                    Driver?.Dispose();
                }
                catch
                {
                    // Ignore driver cleanup errors
                }
            }
        }
    }
}
