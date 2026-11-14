using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public class InventoryPage : BasePage
    {
        private readonly By _title = By.CssSelector("[data-test='title']");
        private readonly By _backpackAddToCartButton = By.CssSelector("[data-test='add-to-cart-sauce-labs-backpack']");
        private readonly By _cartBadge = By.CssSelector("[data-test='shopping-cart-badge']");
        private readonly By _cartLink = By.CssSelector("[data-test='shopping-cart-link']");

        public InventoryPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public bool IsLoaded()
        {
            var urlLoaded = Wait.Until(d => d.Url.Contains("/inventory.html"));
            return urlLoaded && IsDisplayed(_title);
        }

        public string GetTitleText()
        {
            return GetText(_title);
        }

        public InventoryPage AddBackpackToCart()
        {
            Click(_backpackAddToCartButton);
            WaitVisible(_cartBadge);
            return this;
        }

        public string GetCartBadgeCount()
        {
            return GetText(_cartBadge);
        }

        public CartPage OpenCart()
        {
            Click(_cartLink);
            return new CartPage(Driver, Wait);
        }
    }
}
