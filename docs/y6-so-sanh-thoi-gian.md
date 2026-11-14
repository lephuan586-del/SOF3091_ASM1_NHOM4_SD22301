# So sanh Hieu nang Thoi gian Chay Test: Tuan tu vs Song song (Parallel)

## 1. Cau hinh moi truong thu nghiem
- **He dieu hanh**: Windows 11
- **Phien ban .NET**: net8.0
- **Trinh duyet**: Google Chrome (Selenium WebDriver)
- **Framework kiem thu**: NUnit 3 (ParallelScope.Fixtures)
- **So luong ca kiem thu**: 16 test cases (SmokeTests, LoginTests Data-Driven 9 ca, LoginFlowTests, ExplicitWaitTests, CartCheckoutTests)

## 2. Bang ket qua do thoi gian thuc te

| Che do thuc thi | Lan 1 (s) | Lan 2 (s) | Lan 3 (s) | Trung binh (s) |
|---|---|---|---|---|
| **Tuan tu (1 Worker)** | 165.95 | 225.56 | 108.52 | **166.68** |
| **Song song (4 Workers)** | 84.96 | 95.6 | 86.65 | **89.07** |

## 3. Phan tich ket qua va Ket luan
- **Thoi gian trung binh chay tuan tu**: 166.68 giay.
- **Thoi gian trung binh chay song song**: 89.07 giay.
- **Toc do cai thien**: Nhanh hon **46.56%** khi chay song song voi 4 workers.

### Nhieu loi ich cua viec chay song song trong du an thuc te:
1. **Giam thoi gian phan hoi (Feedback Loop)**: Lap trinh vien va tester nhan ket qua kiem thu nhanh hon nhieu lan, dac biet khi bo test case tang len hang tram test.
2. **Toi uu chi phi CI/CD**: Giup tiet kiem gio chay (build minutes) tren GitHub Actions runner.
3. **Phat hien som cac van de xung dot tai nguyen**: Chay song song giup kiem tra xem cac test case co bi phu thuoc du lieu, chia se static state hay gay deadlock/flakiness hay khong.
