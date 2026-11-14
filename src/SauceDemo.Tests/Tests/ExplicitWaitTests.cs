using NUnit.Framework;
using SauceDemo.Tests.Base;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class ExplicitWaitTests : BaseTest
    {
        [Test]
        public void Test_PerformanceGlitchUser_ChoTrangInventoryXuatHien()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            var inventoryPage = loginPage.Login("performance_glitch_user", "secret_sauce");

            Assert.Multiple(() =>
            {
                Assert.That(inventoryPage.IsLoaded(), Is.True);
                Assert.That(inventoryPage.GetTitleText(), Is.EqualTo("Products"));
                Assert.That(inventoryPage.CurrentUrl, Does.Contain("/inventory.html"));
            });
        }

        [Test]
        public void Test_LockedOutUser_ChoThongBaoLoiHienThi()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            loginPage.LoginExpectingFailure("locked_out_user", "secret_sauce");

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True);
                Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Sorry, this user has been locked out."));
            });
        }

        [Test]
        public void Test_SaiMatKhau_ChoThongBaoLoiHienThi()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            loginPage.LoginExpectingFailure("standard_user", "wrong_password");

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True);
                Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
            });
        }
    }
}
