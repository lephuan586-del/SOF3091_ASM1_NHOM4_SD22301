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
    }
}
