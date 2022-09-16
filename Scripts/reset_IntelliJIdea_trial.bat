:: ***************************
:: * Сброс триалки IntelliJ Idea *
:: ***************************
@echo off
color A

echo ^> Remove IntelliJIdea*\config\eval folder
rd /s /q "C:%HOMEPATH%\.IntelliJIdea*\config\eval"
echo .

echo ^> Remove other.xml
del "C:%HOMEPATH%\.IntelliJIdea*\config\options\other.xml"
echo .

echo ^> Remove HKEY_CURRENT_USER\Software\JavaSoft\Prefs\jetbrains\idea
reg delete HKEY_CURRENT_USER\Software\JavaSoft\Prefs\jetbrains\idea /f
echo .

color
pause
echo on