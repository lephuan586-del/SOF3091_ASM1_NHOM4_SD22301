# Loi ich cua Mo hinh Page Object Model (POM)

## 1. Dat van de khi khong dung POM
Khi khong ap dung POM, cac locator (By.Id, By.CssSelector, By.XPath) va hanh dong (Click, SendKeys) bi phan tan khap cac file test.
Neu giao dien nguoi dung (UI) thay doi hoac locator thay doi:
- Tester/Developer phai mo tung file test (`LoginFlowTests.cs`, `ExplicitWaitTests.cs`, `CartTests.cs`, `CheckoutTests.cs`, ...) de tim va sua locator o tat ca cac cho do.
- De gay ra thieu sot, kho bao tri va de phat sinh loi do sua khong dong bo.

## 2. Giai phap khi ap dung POM
Trong POM:
- Moi trang (Page) la mot class rieng (`LoginPage`, `InventoryPage`, `CartPage`, `CheckoutPage`).
- Tat ca locator deu duoc dong goi trong cac truong `private readonly By` cua Page do.
- Cac phuong thuc kiem thu chi tuong tac voi cac method nghiep vu cua Page (`Open()`, `Login()`, `AddBackpackToCart()`, `GetErrorMessage()`).
- Khi giao dien co su thay doi locator, chung ta **CHI CAN SUA O DUY NHAT MOT CHO** tai class Page tuong ung. Tat ca cac ca kiem thu goi toi method cua Page do se tu dong hoat dong binh thuong ma khong can sua bat ky dong code nao trong thu muc `Tests/`.

## 3. Vi du thuc te: Thay doi Locator nut Login tren SauceDemo
Gia su locator cua nut Login duoc doi tu `By.Id("login-button")` sang `By.CssSelector("[data-test='login-button']")`.

Chung ta chi can sua duy nhat tai file `src/SauceDemo.Tests/Pages/LoginPage.cs`.

### Git Diff thuc te (chung minh chi co duy nhat 1 file thay doi):
```diff
diff --git a/src/SauceDemo.Tests/Pages/LoginPage.cs b/src/SauceDemo.Tests/Pages/LoginPage.cs
index 7e862b3..48c3038 100644
--- a/src/SauceDemo.Tests/Pages/LoginPage.cs
+++ b/src/SauceDemo.Tests/Pages/LoginPage.cs
@@ -7,7 +7,7 @@ namespace SauceDemo.Tests.Pages
     {
         private readonly By _usernameInput = By.Id("user-name");
         private readonly By _passwordInput = By.Id("password");
-        private readonly By _loginButton = By.Id("login-button");
+        private readonly By _loginButton = By.CssSelector("[data-test='login-button']");
         private readonly By _errorMessage = By.CssSelector("[data-test='error']");
 
         public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
```

### Ket qua:
Sau khi sua o mot cho duy nhat, tat ca test cases van chay thanh cong (Passed 100%), ma khong phai cham vao bat ky file nao trong `Tests/`.
