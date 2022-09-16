$resPath = "D:\Program\";

$test = @("s1", "2s", "s3s");


$test | ForEach-Object -Process {
    $funcName = "{0}Mapping" -f $_;
    $fileName = "{0}.js" -f $funcName;
    $innerCode = @"
Some code
$_
Some code
"@;
    New-Item -Path $resPath -Name $fileName -ItemType "file" -Value $innerCode
    Write-Host $fileName "created"
}