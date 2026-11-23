$ErrorActionPreference = 'Stop'
$check = Join-Path $PSScriptRoot 'aggregate.ps1'
$path = [IO.Path]::GetTempFileName()
try {
    $rows = @(Get-Content (Join-Path $PSScriptRoot 'aggregates.json') -Raw | ConvertFrom-Json)
    [array]::Reverse($rows)
    $rows | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath $path -Encoding utf8
    & $check -Check -Output $path

    $count = $rows[0].samples
    foreach ($invalidCount in @(($count + 1), ([string]$count))) {
        $rows[0].samples = $invalidCount
        $rows | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath $path -Encoding utf8
        try { & $check -Check -Output $path }
        catch {
            if ($_.Exception.Message -like 'Aggregate differs at *.samples') { continue }
            throw
        }
        throw 'Expected the verifier to reject the invalid sample count'
    }
}
finally { Remove-Item -LiteralPath $path }
