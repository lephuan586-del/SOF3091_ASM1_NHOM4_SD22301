#!/usr/bin/env bash
# Script chay dotnet test 3 lan lien tiep va ghi log vao TestResults/run3.txt

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
RESULTS_DIR="$SCRIPT_DIR/../TestResults"
mkdir -p "$RESULTS_DIR"

LOG_FILE="$RESULTS_DIR/run3.txt"
echo "=== KET QUA CHAY TEST 3 LAN LIEN TIEP ===" > "$LOG_FILE"
echo "Thoi gian: $(date)" >> "$LOG_FILE"
echo "" >> "$LOG_FILE"

ALL_PASSED=1

for i in 1 2 3; do
    echo "Dang chay Lan $i..."
    OUTPUT=$(dotnet test --no-restore 2>&1)
    EXIT_CODE=$?

    if [ $EXIT_CODE -eq 0 ]; then
        STATUS="Passed"
        echo "Lan $i : $STATUS"
    else
        STATUS="Failed"
        echo "Lan $i : $STATUS"
        ALL_PASSED=0
    fi

    echo "Lan $i : $STATUS" >> "$LOG_FILE"
    echo "$OUTPUT" >> "$LOG_FILE"
    echo "--------------------------------------------------" >> "$LOG_FILE"
done

if [ $ALL_PASSED -eq 1 ]; then
    echo "Tong ket: TAT CA 3 LAN DEU THANH CONG (PASSED)"
else
    echo "Tong ket: CO LAN THAT BAI"
fi
echo "Chi tiet log da duoc luu tai: $LOG_FILE"
