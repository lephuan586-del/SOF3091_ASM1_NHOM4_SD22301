using OpenQA.Selenium;

namespace SauceDemo.Tests.Utils
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                while (dir != null && !File.Exists(Path.Combine(dir.FullName, "SauceDemo.sln")) && dir.Parent != null)
                {
                    dir = dir.Parent;
                }

                var rootDir = dir != null && File.Exists(Path.Combine(dir.FullName, "SauceDemo.sln"))
                    ? dir.FullName
                    : AppContext.BaseDirectory;

                var screenshotsDir = Path.Combine(rootDir, "TestResults", "Screenshots");
                if (!Directory.Exists(screenshotsDir))
                {
                    Directory.CreateDirectory(screenshotsDir);
                }

                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                var cleanTestName = string.Join("_", testName.Split(Path.GetInvalidFileNameChars()));
                var fileName = $"{cleanTestName}_{timestamp}.png";
                var filePath = Path.Combine(screenshotsDir, fileName);

                if (driver is ITakesScreenshot takesScreenshot)
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    screenshot.SaveAsFile(filePath);
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"Khong the chup anh man hinh: {ex.Message}");
            }

            return string.Empty;
        }
    }
}
