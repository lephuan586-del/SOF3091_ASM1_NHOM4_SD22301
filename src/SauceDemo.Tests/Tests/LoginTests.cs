using System.Text.Json;
using NUnit.Framework;
using SauceDemo.Tests.Base;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.TestData;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        public static IEnumerable<TestCaseData> GetLoginTestData()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "TestData", "login-data.json");
            var jsonContent = File.ReadAllText(filePath);
            var testCases = JsonSerializer.Deserialize<List<LoginTestCase>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (testCases == null) yield break;

            foreach (var tc in testCases)
            {
                yield return new TestCaseData(tc).SetName($"CaKiemThu_{tc.Name}");
            }
        }

        [Test, TestCaseSource(nameof(GetLoginTestData))]
        public void Test_DangNhap_DataDriven(LoginTestCase testCase)
        {
            var loginPage = new LoginPage(Driver, Wait);
            loginPage.Open(Settings.BaseUrl);

            if (string.Equals(testCase.ExpectedOutcome, "success", StringComparison.OrdinalIgnoreCase))
            {
                var inventoryPage = loginPage.Login(testCase.Username, testCase.Password);

                Assert.Multiple(() =>
                {
                    Assert.That(inventoryPage.IsLoaded(), Is.True, "Trang inventory phai duoc tai thanh cong");
                    Assert.That(inventoryPage.GetTitleText(), Is.EqualTo("Products"));
                    Assert.That(inventoryPage.CurrentUrl, Does.Contain("/inventory.html"));
                });
            }
            else
            {
                loginPage.LoginExpectingFailure(testCase.Username, testCase.Password);

                Assert.Multiple(() =>
                {
                    Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, "Thong bao loi phai hien thi");
                    Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(testCase.ExpectedMessage));
                });
            }
        }
    }
}
