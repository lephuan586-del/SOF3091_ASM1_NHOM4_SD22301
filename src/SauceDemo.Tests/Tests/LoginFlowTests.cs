using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.Tests.Base;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class LoginFlowTests : BaseTest
    {
        [Test]
        public void Test_DangNhapThanhCong_ChuyenDenTrangInventory()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);

            // Locator bang id va data-test theo dung DOM saucedemo
            var usernameInput = Wait.Until(d => d.FindElement(By.Id("user-name")));
            var passwordInput = Driver.FindElement(By.Id("password"));
            var loginButton = Driver.FindElement(By.Id("login-button"));

            usernameInput.SendKeys("standard_user");
            passwordInput.SendKeys("secret_sauce");
            loginButton.Click();

            // Xac nhan vao duoc trang san pham
            Wait.Until(d => d.Url.Contains("/inventory.html"));
            var titleElement = Wait.Until(d => d.FindElement(By.CssSelector("[data-test='title']")));

            Assert.Multiple(() =>
            {
                Assert.That(Driver.Url, Does.Contain("/inventory.html"));
                Assert.That(titleElement.Displayed, Is.True);
                Assert.That(titleElement.Text, Is.EqualTo("Products"));
            });
        }

        [Test]
        public void Test_DangNhap_ThemSanPhamVaoGio_MoGioHang_XacNhanSanPham()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);

            // Dang nhap
            var usernameInput = Wait.Until(d => d.FindElement(By.Id("user-name")));
            var passwordInput = Driver.FindElement(By.Id("password"));
            var loginButton = Driver.FindElement(By.Id("login-button"));

            usernameInput.SendKeys("standard_user");
            passwordInput.SendKeys("secret_sauce");
            loginButton.Click();

            // Cho den trang inventory
            Wait.Until(d => d.Url.Contains("/inventory.html"));

            // Them 1 san pham vao gio
            var addToCartButton = Wait.Until(d => d.FindElement(By.CssSelector("[data-test='add-to-cart-sauce-labs-backpack']")));
            addToCartButton.Click();

            // Xac nhan badge gio hang = 1
            var cartBadge = Wait.Until(d => d.FindElement(By.CssSelector("[data-test='shopping-cart-badge']")));
            Assert.That(cartBadge.Text, Is.EqualTo("1"));

            // Mo gio hang
            var cartLink = Driver.FindElement(By.CssSelector("[data-test='shopping-cart-link']"));
            cartLink.Click();

            // Xac nhan chuyen sang trang cart va san pham ton tai trong gio hang
            Wait.Until(d => d.Url.Contains("/cart.html"));
            var itemName = Wait.Until(d => d.FindElement(By.CssSelector("[data-test='inventory-item-name']")));

            Assert.Multiple(() =>
            {
                Assert.That(Driver.Url, Does.Contain("/cart.html"));
                Assert.That(itemName.Displayed, Is.True);
                Assert.That(itemName.Text, Is.EqualTo("Sauce Labs Backpack"));
            });
        }
    }
}
