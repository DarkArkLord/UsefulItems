$rootFolder = "D:\Program\Adacta\Implementation_sogaz\configuration\@config-sogaz\AgentAgreement\etlService\AgentAgreementImportEtlService\sinkMappings"
$fileExt = "*.js"
$filesList = Get-ChildItem -Recurse -Path $rootFolder -Include $fileExt -File

$findString = ") {"
$insertString = "    debugger;"

foreach($filePath in $filesList)
{
    $fileStrings = (Get-Content -Path $filePath -Raw).Split("`n")
    $modFile = @()
    $wasUpdated = 0
    foreach($curStr in $fileStrings) {
        $modFile += $curStr;
        #Write-Host $curStr
        $findIndex = $curStr.IndexOf($findString);
        #Write-Host $findIndex
        if ($findIndex -ge 0 -and $wasUpdated -eq 0) {
            $modFile += $insertString
            $wasUpdated += 1
        }
    }
    $modFile = ($modFile -join "").Trim()
    #Write-Host $modFile
    Set-Content $filePath $modFile -Force
    Write-Host $filePath "Updated"
    #break
}