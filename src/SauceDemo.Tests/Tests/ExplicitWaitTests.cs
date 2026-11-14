using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.Tests.Base;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class ExplicitWaitTests : BaseTest
    {
        [Test]
        public void Test_PerformanceGlitchUser_ChoTrangInventoryXuatHien()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);

            var usernameInput = Wait.Until(d => d.FindElement(By.Id("user-name")));
            var passwordInput = Driver.FindElement(By.Id("password"));
            var loginButton = Driver.FindElement(By.Id("login-button"));

            // performance_glitch_user co do tre khi tai trang
            usernameInput.SendKeys("performance_glitch_user");
            passwordInput.SendKeys("secret_sauce");
            loginButton.Click();

            // Cho tuong minh URL chuyen den /inventory.html va tieu de hien thi
            var isInventoryUrl = Wait.Until(d => d.Url.Contains("/inventory.html"));
            var titleElement = Wait.Until(d => {
                var el = d.FindElement(By.CssSelector("[data-test='title']"));
                return el.Displayed ? el : null;
            });

            Assert.Multiple(() =>
            {
                Assert.That(isInventoryUrl, Is.True);
                Assert.That(titleElement, Is.Not.Null);
                Assert.That(titleElement!.Text, Is.EqualTo("Products"));
            });
        }

        [Test]
        public void Test_LockedOutUser_ChoThongBaoLoiHienThi()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);

            var usernameInput = Wait.Until(d => d.FindElement(By.Id("user-name")));
            var passwordInput = Driver.FindElement(By.Id("password"));
            var loginButton = Driver.FindElement(By.Id("login-button"));

            usernameInput.SendKeys("locked_out_user");
            passwordInput.SendKeys("secret_sauce");
            loginButton.Click();

            // Cho tuong minh phan tu thong bao loi xuat hien
            var errorElement = Wait.Until(d => {
                var el = d.FindElement(By.CssSelector("[data-test='error']"));
                return el.Displayed ? el : null;
            });

            Assert.Multiple(() =>
            {
                Assert.That(errorElement, Is.Not.Null);
                Assert.That(errorElement!.Text, Is.EqualTo("Epic sadface: Sorry, this user has been locked out."));
            });
        }

        [Test]
        public void Test_SaiMatKhau_ChoThongBaoLoiHienThi()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);

            var usernameInput = Wait.Until(d => d.FindElement(By.Id("user-name")));
            var passwordInput = Driver.FindElement(By.Id("password"));
            var loginButton = Driver.FindElement(By.Id("login-button"));

            usernameInput.SendKeys("standard_user");
            passwordInput.SendKeys("wrong_password");
            loginButton.Click();

            var errorElement = Wait.Until(d => {
                var el = d.FindElement(By.CssSelector("[data-test='error']"));
                return el.Displayed ? el : null;
            });

            Assert.Multiple(() =>
            {
                Assert.That(errorElement, Is.Not.Null);
                Assert.That(errorElement!.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
            });
        }
    }
}
