# Script do thoi gian chay Tuan tu (1 worker) vs Song song (4 workers)

$docPath = Join-Path $PSScriptRoot "..\docs\y6-so-sanh-thoi-gian.md"
$docDir = Split-Path $docPath
if (-not (Test-Path $docDir)) {
    New-Item -ItemType Directory -Path $docDir -Force | Out-Null
}

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "   BAT DAU BENCHMARK: TUAN TU VS SONG SONG        " -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan

$seqTimes = @()
$parTimes = @()

# 1. Chay tuan tu (1 worker)
Write-Host "`n>>> [1/2] Dang do che do TUAN TU (1 Worker)..." -ForegroundColor Yellow
for ($i = 1; $i -le 3; $i++) {
    Write-Host "   - Chay Tuan tu lan $i..." -NoNewline
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    $output = dotnet test --no-restore -- NUnit.NumberOfTestWorkers=1 2>&1
    $sw.Stop()
    $seconds = [Math]::Round($sw.Elapsed.TotalSeconds, 2)
    $seqTimes += $seconds
    Write-Host " Hoan thanh: $seconds giay (ExitCode: $LASTEXITCODE)" -ForegroundColor Green
}

# 2. Chay song song (4 workers)
Write-Host "`n>>> [2/2] Dang do che do SONG SONG (4 Workers)..." -ForegroundColor Yellow
for ($i = 1; $i -le 3; $i++) {
    Write-Host "   - Chay Song song lan $i..." -NoNewline
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    $output = dotnet test --no-restore -- NUnit.NumberOfTestWorkers=4 2>&1
    $sw.Stop()
    $seconds = [Math]::Round($sw.Elapsed.TotalSeconds, 2)
    $parTimes += $seconds
    Write-Host " Hoan thanh: $seconds giay (ExitCode: $LASTEXITCODE)" -ForegroundColor Green
}

$seqAvg = [Math]::Round(($seqTimes | Measure-Object -Average).Average, 2)
$parAvg = [Math]::Round(($parTimes | Measure-Object -Average).Average, 2)
$speedup = [Math]::Round((($seqAvg - $parAvg) / $seqAvg) * 100, 2)

$content = @"
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
| **Tuan tu (1 Worker)** | $($seqTimes[0]) | $($seqTimes[1]) | $($seqTimes[2]) | **$seqAvg** |
| **Song song (4 Workers)** | $($parTimes[0]) | $($parTimes[1]) | $($parTimes[2]) | **$parAvg** |

## 3. Phan tich ket qua va Ket luan
- **Thoi gian trung binh chay tuan tu**: $seqAvg giay.
- **Thoi gian trung binh chay song song**: $parAvg giay.
- **Toc do cai thien**: Nhanh hon **$speedup%** khi chay song song voi 4 workers.

### Nhieu loi ich cua viec chay song song trong du an thuc te:
1. **Giam thoi gian phan hoi (Feedback Loop)**: Lap trinh vien va tester nhan ket qua kiem thu nhanh hon nhieu lan, dac biet khi bo test case tang len hang tram test.
2. **Toi uu chi phi CI/CD**: Giup tiet kiem gio chay (build minutes) tren GitHub Actions runner.
3. **Phat hien som cac van de xung dot tai nguyen**: Chay song song giup kiem tra xem cac test case co bi phu thuoc du lieu, chia se static state hay gay deadlock/flakiness hay khong.
"@

Set-Content -Path $docPath -Value $content -Encoding utf8
Write-Host "`n==================================================" -ForegroundColor Cyan
Write-Host "BENCHMARK HOAN TAT!" -ForegroundColor Green
Write-Host "Ket qua: Tuan tu = $seqAvg s, Song song = $parAvg s (Nhanh hon $speedup%)" -ForegroundColor Green
Write-Host "File bao cao da duoc ghi tai: $docPath" -ForegroundColor Green
Write-Host "==================================================" -ForegroundColor Cyan
