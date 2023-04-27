using InventoryManagement.HostBuilders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace InventoryManagement;

/// <summary>
/// Главный класс, с которого начинается работа приложенияЫ
/// </summary>
public partial class App : Application
{
    public IHost AppHost { get; private set; }

    /// <summary>
    /// Метод, который вызывается перед началом работы приложения
    /// </summary>
    /// <param name="e"></param>
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        AppHost = ApplicationHostBuilder.CreateDefaultBuilder(e.Args).Build();

        //Получаем окно авторизации из контейнера
        AuthorizationWindow mainWindow = AppHost.Services.GetRequiredService<AuthorizationWindow>();

        //Показывем его
        mainWindow.Show();

        //Запускаем хост
        await AppHost.StartAsync();
    }

    /// <summary>
    /// Метод, который вызывается перед завершением работы приложения
    /// </summary>
    /// <param name="e"></param>
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        //Останавливаем хост
        await AppHost.StopAsync();

        //Освобождаем ресурсы, который хост использовал при работе
        AppHost.Dispose();
    }
}
