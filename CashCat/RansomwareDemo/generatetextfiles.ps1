$TotalRuns = 150

for($i = 0; $i -lt $TotalRuns; $i++)
{
    $FileName = ((Get-Random).TOString()+".txt")
    "test" >> "$FileName"
}