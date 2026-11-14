using NUnit.Framework;
using SauceDemo.Tests.Base;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class LoginFlowTests : BaseTest
    {
        [Test]
        public void Test_DangNhapThanhCong_ChuyenDenTrangInventory()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            var inventoryPage = loginPage.Login("standard_user", "secret_sauce");

            Assert.Multiple(() =>
            {
                Assert.That(inventoryPage.IsLoaded(), Is.True);
                Assert.That(inventoryPage.GetTitleText(), Is.EqualTo("Products"));
                Assert.That(inventoryPage.CurrentUrl, Does.Contain("/inventory.html"));
            });
        }

        [Test]
        public void Test_DangNhap_ThemSanPhamVaoGio_MoGioHang_XacNhanSanPham()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            var inventoryPage = loginPage.Login("standard_user", "secret_sauce");
            Assert.That(inventoryPage.IsLoaded(), Is.True);

            inventoryPage.AddBackpackToCart();
            Assert.That(inventoryPage.GetCartBadgeCount(), Is.EqualTo("1"));

            var cartPage = inventoryPage.OpenCart();

            Assert.Multiple(() =>
            {
                Assert.That(cartPage.IsLoaded(), Is.True);
                Assert.That(cartPage.IsItemDisplayed(), Is.True);
                Assert.That(cartPage.GetFirstItemName(), Is.EqualTo("Sauce Labs Backpack"));
                Assert.That(cartPage.CurrentUrl, Does.Contain("/cart.html"));
            });
        }
    }
}
