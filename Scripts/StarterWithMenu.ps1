# Абсолютный путь к папкам
$MonoPath = 'D:\Program\Sogaz\mono';
$ImplPath = 'D:\Program\Sogaz\Implementation';

# Набор команд
$Commands = @{
    Move  = @{
        Mono     = ('cd ' + $MonoPath);
        Impl     = ('cd ' + $ImplPath);
        ImplConf = ('cd ' + $ImplPath + '\configuration');
        ImplPlugins = ('cd ' + $ImplPath + '\plugins');
    };
    Run   = @{
        Server = '.\build.ps1 -RunServer';
        Docker = 'docker-compose up --scale server=0';
    };
    Kill  = @{
        Docker = 'docker-compose stop';
        Server = 'Get-Process | ? ProcessName -Like "*AdInsure*" | ForEach-Object -Process { kill $_.Id }';
    };
    Build = @{
        CleanGit = 'git clean -dfx';
        Install  = 'yarn install --ignore-scripts';
        RestoreDotnet     = 'dotnet restore --interactive';
        Impl     = '.\build.ps1 -Build -ExecuteScripts -BuildPrintouts';
    };
}

# Кнфигурация вкладки
class TabConfig {
    [string]$Name
    # Массив выполняемых при создании команд
    [string[]]$ToExecute
}

$TabsConfig = @{
    Docker        = [TabConfig]@{
        Name      = 'Docker';
        ToExecute = @(
            $Commands.Move.Impl, 
            $Commands.Run.Docker
        )
    };
    Killer        = [TabConfig]@{
        Name      = 'Killer';
        ToExecute = @(
            $Commands.Kill.Server,
            $Commands.Move.Impl, 
            $Commands.Kill.Docker
        )
    };
    Server        = [TabConfig]@{
        Name      = 'Server';
        ToExecute = @(
            $Commands.Move.Mono, 
            $Commands.Run.Server
        )
    };
    ReBuildServer = [TabConfig]@{
        Name      = 'Server-builder';
        ToExecute = @(
            $Commands.Kill.Server,
            $Commands.Move.ImplConf, 
            $Commands.Build.CleanGit, 
            $Commands.Move.Impl, 
            $Commands.Build.Install, 
            $Commands.Move.ImplPlugins,
            $Commands.Build.RestoreDotnet, 
            $Commands.Move.Impl, 
            $Commands.Build.Impl
        )
    };
};

# Ожидание способности вкладки исполнять (без этого не работает)
function Wait-Tab {
    param(
        [Microsoft.PowerShell.Host.ISE.PowerShellTab]$tab
    )
    while (-not $tab.CanInvoke) { 
        Start-Sleep -Milliseconds 100
    }
}

# Создание вкладки по конфигурации
function Create-Tab {
    param (
        [TabConfig]$config
    )
    $tab = $psISE.PowerShellTabs | ? DisplayName -EQ $config.Name;
    if (-not $tab) {
        $tab = $psISE.PowerShellTabs.Add();
        $tab.DisplayName = $config.Name;
    }

    foreach ($script in $config.ToExecute) {
        Wait-Tab $tab
        $tab.Invoke($script);
    }
    return $tab;
}

class MenuItem {
    [string]$Title
    [string]$Input
    [string]$Value
    [Action]$Action
}

$MenuConfig = @(
    [MenuItem]@{
        Title  = "Start Server";
        Input  = "s"
        Action = { 
            $temp = Create-Tab $TabsConfig.Docker;
            $temp = Create-Tab $TabsConfig.Server;
        }
    };
    [MenuItem]@{
        Title  = "Rebuild Server";
        Input  = "r"
        Action = { 
            $temp = Create-Tab $TabsConfig.ReBuildServer;
            Wait-Tab $temp;
            $temp = Create-Tab $TabsConfig.Server;
        }
    };
    [MenuItem]@{
        Title  = "Kill Server";
        Input  = "k"
        Action = { 
            $temp = Create-Tab $TabsConfig.Killer;
        }
    };
    [MenuItem]@{
        Title  = "Exit";
        Input  = "q"
        Action = { exit }
    };
);

function Make-String-Long {
    param (
        [string]$Content,
        [System.Int32]$Length,
        [string]$Fill,
        [string]$Border,
        [switch]$Middle
    )
    if($Middle) {
        $t = $Content.Length + [Math]::Floor(($Length - $Content.Length) / 2);
        $Content = $Content.PadLeft($t, $Fill);
    }
    return "{0}{1}{2}{1}{0}" -f $Border, $Fill, $Content.PadRight($Length, $Fill);
}

function Init-Menu-Strings {
    $itemMaxLen = 0;
    foreach ($item in $MenuConfig) {
        $item.Value = "{0} -> {1}" -f $item.Input, $item.Title
        $itemMaxLen = [Math]::Max($itemMaxLen, $item.Value.Length);
    }

    $BorderLine = Make-String-Long "" $itemMaxLen "=" "+"
    $MenuStrings = New-Object System.Collections.Generic.List[System.Object]
    $MenuStrings.Add($BorderLine);
    $MenuStrings.Add((Make-String-Long "Menu" $itemMaxLen " " "|" -Middle));
    $MenuStrings.Add($BorderLine);
    foreach ($item in $MenuConfig) {
        $MenuStrings.Add((Make-String-Long $item.Value $itemMaxLen " " "|"));
    }
    $MenuStrings.Add($BorderLine);

    return $MenuStrings;
}

$MenuStrings = Init-Menu-Strings

while ($true) {
    foreach ($item in $MenuStrings) {
        Write-Host $item -ForegroundColor Yellow
    }
    $inValue = Read-Host "> "
    $inItems = $MenuConfig | ? Input -EQ $inValue;
    if ($inItems) {
        Write-Host "Selected item:" $inItems.Title -ForegroundColor Green
        $inItems.Action.Invoke();
    }
    else {
        Write-Host "Incorrect command:" $inValue -ForegroundColor Red
    }
    # Write-Host "Press any key to continue..."
    # [System.Console]::ReadKey();
}
