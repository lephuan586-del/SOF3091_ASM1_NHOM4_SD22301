using NUnit.Framework;
using SauceDemo.Tests.Base;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class CartCheckoutTests : BaseTest
    {
        [Test]
        public void Test_MuaHang_HoanTatDonHang()
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            var inventoryPage = loginPage.Login("standard_user", "secret_sauce");
            Assert.That(inventoryPage.IsLoaded(), Is.True);

            inventoryPage.AddBackpackToCart();
            Assert.That(inventoryPage.GetCartBadgeCount(), Is.EqualTo("1"));

            var cartPage = inventoryPage.OpenCart();
            Assert.That(cartPage.IsLoaded(), Is.True);
            Assert.That(cartPage.GetFirstItemName(), Is.EqualTo("Sauce Labs Backpack"));

            var checkoutPage = cartPage.GoToCheckout();
            checkoutPage.FillInformation("Nguyen", "Van A", "700000");
            checkoutPage.ClickFinish();

            Assert.Multiple(() =>
            {
                Assert.That(checkoutPage.IsOrderComplete(), Is.True, "Don hang phai chuyen den man hinh thanh cong");
                Assert.That(checkoutPage.GetCompleteHeaderText(), Is.EqualTo("Thank you for your order!"));
            });
        }
    }
}
