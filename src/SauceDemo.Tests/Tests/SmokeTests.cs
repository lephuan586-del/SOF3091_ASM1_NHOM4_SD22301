using NUnit.Framework;
using SauceDemo.Tests.Base;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class SmokeTests : BaseTest
    {
        [Test]
        public void Test_TrangChu_TieuDeChinhXac()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);
            Wait.Until(d => !string.IsNullOrEmpty(d.Title));

            Assert.That(Driver.Title, Is.EqualTo("Swag Labs"));
        }
    }
}
