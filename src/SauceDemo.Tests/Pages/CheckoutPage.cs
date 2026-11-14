using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public class CheckoutPage : BasePage
    {
        private readonly By _firstNameInput = By.Id("first-name");
        private readonly By _lastNameInput = By.Id("last-name");
        private readonly By _postalCodeInput = By.Id("postal-code");
        private readonly By _continueButton = By.Id("continue");
        private readonly By _finishButton = By.Id("finish");
        private readonly By _completeHeader = By.CssSelector("[data-test='complete-header']");

        public CheckoutPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public CheckoutPage FillInformation(string firstName, string lastName, string postalCode)
        {
            Type(_firstNameInput, firstName);
            Type(_lastNameInput, lastName);
            Type(_postalCodeInput, postalCode);
            Click(_continueButton);
            return this;
        }

        public CheckoutPage ClickFinish()
        {
            Click(_finishButton);
            return this;
        }

        public bool IsOrderComplete()
        {
            return Wait.Until(d => d.Url.Contains("/checkout-complete.html")) && IsDisplayed(_completeHeader);
        }

        public string GetCompleteHeaderText()
        {
            return GetText(_completeHeader);
        }
    }
}
