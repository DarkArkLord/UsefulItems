param([switch]$kill, [switch]$rebuild)

# Абсолютный путь к папкам
$MonoPath = 'D:\Program\Sogaz\mono';
$ImplPath = 'D:\Program\Sogaz\Implementation';

# Набор команд
$Commands = @{
    Move = @{
        Mono = ('cd ' + $MonoPath);
        Impl = ('cd ' + $ImplPath);
        ImplConf = ('cd ' + $ImplPath + '\configuration');
    };
    Run = @{
        Server = '.\build.ps1 -RunServer';
        Docker = 'docker-compose up --scale server=0';
    };
    Kill = @{
        Docker = 'docker-compose stop';
        Server = 'Get-Process | ? ProcessName -EQ "AdInsure.Server" | select Id | kill';
    };
    Build = @{
        CleanGit = 'git clean -dfx';
        Install = 'yarn install';
        Impl = '.\build.ps1 -Build -ExecuteScripts -BuildPrintouts';
    };
}

# Кнфигурация вкладки
class TabConfig {
    [string]$Name
	# Массив выполняемых при создании команд
    [string[]]$ToExecute
}

$TabsConfig = @{
    Docker = [TabConfig]@{
        Name = 'Docker';
        ToExecute = @(
            $Commands.Move.Impl, 
            $Commands.Run.Docker
        )
    };
    Killer = [TabConfig]@{
        Name = 'Killer';
        ToExecute = @(
            $Commands.Kill.Server,
            $Commands.Move.Impl, 
            $Commands.Kill.Docker
        )
    };
    Server = [TabConfig]@{
        Name = 'Server';
        ToExecute = @(
            $Commands.Move.Mono, 
            $Commands.Run.Server
        )
    };
    ReBuildServer = [TabConfig]@{
        Name = 'Server-builder';
        ToExecute = @(
            $Commands.Kill.Server,
            $Commands.Move.ImplConf, 
            $Commands.Build.CleanGit, 
            $Commands.Move.Impl, 
            $Commands.Build.Install, 
            $Commands.Build.Impl
        )
    };
};

# Ожидание способности вкладки исполнять (без этого не работает)
function Wait-Tab {
    param(
        [Microsoft.PowerShell.Host.ISE.PowerShellTab]$tab
    )
    while(-not $tab.CanInvoke) { 
        Start-Sleep -Milliseconds 100
    }
}

# Создание вкладки по конфигурации
function Create-Tab {
    param (
        [TabConfig]$config
    )
    $tab = $psISE.PowerShellTabs | ? DisplayName -EQ $config.Name;
    if(-not $tab) {
        $tab = $psISE.PowerShellTabs.Add();
        $tab.DisplayName = $config.Name;
    }

    foreach($script in $config.ToExecute) {
        Wait-Tab $tab
        $tab.Invoke($script);
    }
    return $tab;
}

if($kill)
{
    $temp = Create-Tab $TabsConfig.Killer;
}
else 
{
    if($rebuild)
    {
        $temp = Create-Tab $TabsConfig.ReBuildServer;
        Wait-Tab $temp;
        $temp = Create-Tab $TabsConfig.Server;
    }
    else
    {
        $temp = Create-Tab $TabsConfig.Docker;
        $temp = Create-Tab $TabsConfig.Server;
    }
}

# TODO:
# - Сделать, чтобы при закрытии вкладки закрывались запущенные процессы
# - Добавить менюшку