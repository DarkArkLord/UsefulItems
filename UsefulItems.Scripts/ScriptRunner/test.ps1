Add-Type -AssemblyName PresentationFramework
[xml]$xaml = Get-Content -Path .\UsefulItems.Scripts\ScriptRunner\form.xaml
$reader = (New-Object System.Xml.XmlNodeReader $xaml)
$window = [Windows.Markup.XamlReader]::Load($reader)
$xaml.SelectNodes("//*[@*[contains(translate(name(.),'n','N'),'Name')]]") | %{
    Set-Variable -Name ($_.Name) -Value $window.FindName($_.Name) -Scope Global
}
$btnExit.Add_Click({
    $window.Close()
})
$window.ShowDialog()