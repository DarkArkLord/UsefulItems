Add-Type -AssemblyName PresentationFramework
[xml]$xaml = Get-Content -Path .\UsefulItems.Scripts\ScriptRunner\form.xml
$reader = (New-Object System.Xml.XmlNodeReader $xaml)
$window = [Windows.Markup.XamlReader]::Load($reader)
$window.ShowDialog()
