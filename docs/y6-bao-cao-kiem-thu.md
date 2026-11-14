# Huong dan Xem va Xuat Bao cao Kiem thu (.trx)

## 1. Dinh dang bao cao .trx la gi?
`.trx` (Visual Studio Test Results File) la dinh dang bao cao kiem thu chuan cua he sinh thai .NET va Microsoft Test Platform.
File bao cao chua day du thong tin:
- Tong so test case chay, so test Pass, Fail, Skipped.
- Thoi gian bat dau, ket thuc, thoi luong thuc thi cua tung test.
- Stack trace, Error message chi tiet khi test fail.
- Cac tep dinh kem (Test Attachments), bao gom anh chup man hinh khi test bi loi.

## 2. Cach tao bao cao .trx
Lenh thuc thi:
```powershell
dotnet test --logger "trx;LogFileName=test-results.trx" --results-directory ./TestResults
```
File bao cao se duoc tao tai: `TestResults/test-results.trx`.

## 3. Cach mo va xem bao cao
1. **Tren Visual Studio**: Double click truc tiep vao file `test-results.trx` hoac mo thong qua menu `Test > Test Results`. Visual Studio se mo giao dien truc quan hien thi tung test case, kem button xem truc tiep file anh chup man hinh dinh kem.
2. **Tren VS Code**: Cai dat extension `TRX Test Viewer` hoac `.NET Core Test Explorer`.
3. **Tren GitHub Actions**: Workflow tu dong upload toan bo thu muc `TestResults` len Artifacts sau moi lan chay (ke ca khi test failed nho `if: always()`). Ban co the tai artifact `test-results.zip` tai tab Summary cua moi workflow run.
