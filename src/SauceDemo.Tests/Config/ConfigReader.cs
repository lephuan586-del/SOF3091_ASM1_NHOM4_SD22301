using Microsoft.Extensions.Configuration;

namespace SauceDemo.Tests.Config
{
    public static class ConfigReader
    {
        public static TestSettings Settings { get; }

        static ConfigReader()
        {
            var basePath = AppContext.BaseDirectory;
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            Settings = new TestSettings();

            var baseUrl = configuration["TestSettings:BaseUrl"] ?? configuration["TestSettings__BaseUrl"] ?? configuration["BaseUrl"];
            if (!string.IsNullOrEmpty(baseUrl)) Settings.BaseUrl = baseUrl;

            var browser = configuration["TestSettings:Browser"] ?? configuration["TestSettings__Browser"] ?? configuration["Browser"];
            if (!string.IsNullOrEmpty(browser)) Settings.Browser = browser;

            var timeout = configuration["TestSettings:TimeoutSeconds"] ?? configuration["TestSettings__TimeoutSeconds"] ?? configuration["TimeoutSeconds"];
            if (int.TryParse(timeout, out var timeoutVal)) Settings.TimeoutSeconds = timeoutVal;

            var headless = configuration["TestSettings:Headless"] ?? configuration["TestSettings__Headless"] ?? configuration["Headless"];
            if (bool.TryParse(headless, out var headlessVal)) Settings.Headless = headlessVal;
        }
    }
}
