namespace SauceDemo.Tests.Config
{
    public class TestSettings
    {
        public string BaseUrl { get; set; } = "https://www.saucedemo.com/";
        public string Browser { get; set; } = "Chrome";
        public int TimeoutSeconds { get; set; } = 10;
        public bool Headless { get; set; } = false;
    }
}
