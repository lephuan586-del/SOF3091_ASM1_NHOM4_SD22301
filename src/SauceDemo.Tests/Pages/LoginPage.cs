using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By _usernameInput = By.Id("user-name");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.Id("login-button");
        private readonly By _errorMessage = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public LoginPage Open(string baseUrl)
        {
            Driver.Navigate().GoToUrl(baseUrl);
            WaitVisible(_loginButton);
            return this;
        }

        public InventoryPage Login(string username, string password)
        {
            Type(_usernameInput, username);
            Type(_passwordInput, password);
            Click(_loginButton);
            return new InventoryPage(Driver, Wait);
        }

        public LoginPage LoginExpectingFailure(string username, string password)
        {
            Type(_usernameInput, username);
            Type(_passwordInput, password);
            Click(_loginButton);
            WaitVisible(_errorMessage);
            return this;
        }

        public string GetErrorMessage()
        {
            return GetText(_errorMessage);
        }

        public bool IsErrorMessageDisplayed()
        {
            return IsDisplayed(_errorMessage);
        }
    }
}
