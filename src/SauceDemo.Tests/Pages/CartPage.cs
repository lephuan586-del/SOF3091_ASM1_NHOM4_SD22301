using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public class CartPage : BasePage
    {
        private readonly By _cartItemName = By.CssSelector("[data-test='inventory-item-name']");
        private readonly By _checkoutButton = By.CssSelector("[data-test='checkout']");

        public CartPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public bool IsLoaded()
        {
            return Wait.Until(d => d.Url.Contains("/cart.html"));
        }

        public string GetFirstItemName()
        {
            return GetText(_cartItemName);
        }

        public bool IsItemDisplayed()
        {
            return IsDisplayed(_cartItemName);
        }

        public CheckoutPage GoToCheckout()
        {
            Click(_checkoutButton);
            return new CheckoutPage(Driver, Wait);
        }
    }
}
