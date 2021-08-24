:: ***************************
:: * Сброс триалки ReSharper *
:: ***************************
@echo off

color A
echo ^> Remove JetBrains folder from Roaming
rd /s /q "C:\Users\%username%\AppData\Roaming\JetBrains"
echo .

echo ^> Remove HKCU\Software\JetBrains
reg delete HKEY_CURRENT_USER\Software\JetBrains /f
echo .

echo ^> Remove HKLM\Software\JetBrains
reg delete HKEY_LOCAL_MACHINE\Software\JetBrains /f
echo .

echo ^> Remove HKCU\Software\Microsoft\Windows\CurrentVersion\Ext\Settings\{9656c84c-e0b4-4454-996d-977eabdf9e86}
reg delete HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Ext\Settings\{9656c84c-e0b4-4454-996d-977eabdf9e86} /f
echo .

color
pause
echo on