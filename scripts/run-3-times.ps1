# Script chay dotnet test 3 lan lien tiep va ghi log vao TestResults/run3.txt

$resultsDir = Join-Path $PSScriptRoot "..\TestResults"
if (-not (Test-Path $resultsDir)) {
    New-Item -ItemType Directory -Path $resultsDir -Force | Out-Null
}

$logFile = Join-Path $resultsDir "run3.txt"
"=== KET QUA CHAY TEST 3 LAN LIEN TIEP ===" | Out-File -FilePath $logFile -Encoding utf8
"Thoi gian: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" | Out-File -FilePath $logFile -Append -Encoding utf8
"" | Out-File -FilePath $logFile -Append -Encoding utf8

$allPassed = $true

for ($i = 1; $i -le 3; $i++) {
    Write-Host "`nDang chay Lan $i..." -ForegroundColor Cyan
    $output = dotnet test --no-restore 2>&1
    $exitCode = $LASTEXITCODE

    if ($exitCode -eq 0) {
        $status = "Passed"
        Write-Host "Lan $i : $status" -ForegroundColor Green
    } else {
        $status = "Failed"
        Write-Host "Lan $i : $status" -ForegroundColor Red
        $allPassed = $false
    }

    "Lan $i : $status" | Out-File -FilePath $logFile -Append -Encoding utf8
    $output | Out-File -FilePath $logFile -Append -Encoding utf8
    "--------------------------------------------------" | Out-File -FilePath $logFile -Append -Encoding utf8
}

Write-Host "`nTong ket: $(if ($allPassed) { 'TAT CA 3 LAN DEU THANH CONG (PASSED)' } else { 'CO LAN THAT BAI' })" -ForegroundColor $(if ($allPassed) { 'Green' } else { 'Red' })
Write-Host "Chi tiet log da duoc luu tai: $logFile"
