# Абсолютный путь к папкам
$MonoPath = 'D:\Program\Sogaz\mono';
$ImplPath = 'D:\Program\Sogaz\Implementation';

# Набор команд
$Commands = @{
    Move = @{
        Mono = ('cd ' + $MonoPath);
        Client = ('cd ' + $MonoPath + '\client');
        Implementation = ('cd ' + $ImplPath);
    };
    Run = @{
        Server = '.\build.ps1 -RunServer';
        IIS = '.\build.ps1 -RunIS';
        Client = 'yarn run start';
    };
}

# Кнфигурация вкладки
class TabConfig {
    [string]$Name
	# Массив выполняемых при создании команд
    [string[]]$ToExecute
}

$TabsConfig = @{
    Server = [TabConfig]@{
        Name = 'Server';
        ToExecute = @($Commands.Move.Mono, $Commands.Run.Server)
    };
    IIS = [TabConfig]@{
        Name = 'IIS';
        ToExecute = @($Commands.Move.Mono, $Commands.Run.IIS)
    };
    Client = [TabConfig]@{
        Name = 'Client';
        ToExecute = @($Commands.Move.Client, $Commands.Run.Client)
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

    $tab = $psISE.PowerShellTabs.Add();
    $tab.DisplayName = $config.Name;

    foreach($script in $config.ToExecute) {
        Wait-Tab $tab
        $tab.Invoke($script);
    }
    return $tab;
}

$Tabs = @{
	Server = Create-Tab $TabsConfig.Server;
    IIS = Create-Tab $TabsConfig.IIS;
    Client = Create-Tab $TabsConfig.Client;
};
# TODO:
# - Сделать, чтобы при закрытии вкладки закрывались запущенные процессы