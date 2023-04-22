using InventoryManagement.HostBuilders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace InventoryManagement;

public partial class App : Application
{
    public IHost AppHost { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        AppHost = ApplicationHostBuilder.CreateDefaultBuilder(e.Args).Build();

        AuthorizationWindow mainWindow = AppHost.Services.GetRequiredService<AuthorizationWindow>();
        mainWindow.Show();

        await AppHost.StartAsync();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        await AppHost.StopAsync();
        AppHost.Dispose();
    }
}
