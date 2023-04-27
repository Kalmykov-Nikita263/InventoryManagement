using InventoryManagement.Domain.Repository.Abstractions;
using InventoryManagement.Domain.Repository.EntityFramework;
using InventoryManagement.Domain;
using InventoryManagement.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InventoryManagement.Services.Abstractions;
using InventoryManagement.Services.Implementations;

namespace InventoryManagement.HostBuilders;

/// <summary>
/// Класс, который создаёт хост-приложение
/// </summary>
public class ApplicationHostBuilder
{
    /// <summary>
    /// Настраивает хост-приложение, регистрируя сервисы в DI контейнере
    /// </summary>
    /// <param name="args">Аргументы, которые приложение получает при запуске</param>
    /// <returns>Объект IHostBuilder</returns>
    public static IHostBuilder CreateDefaultBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
        {
            //Сопоставляем ветку "ConnectionStrings" файла appsettings.json, с классом конфигурации ConfigurationHelper
            hostContext.Configuration.Bind("ConnectionStrings", new ConfigurationHelper());
            
            //Регистрируем классы для работы с БД в DI контейнер. Сопоставляем их интерфейс с конкретной реализацией
            services.AddTransient<IInventoryRepository, EFInventoryRepository>();
            services.AddTransient<IAssetRepository, EFAssetRepository>();

            //Регистрируем классы для авторизации и управления пользователями в DI контейнер. Сопоставляем их интерфейс с конкретной реализацией
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IUserService, UserService>();

            //Регистриуем DataManager
            services.AddTransient<DataManager>();

            //Регистрируем окно авторизации
            services.AddSingleton<AuthorizationWindow>();

            //Добавляем контекст БД в контейнер. Используем Lazy Loading для автоматической загрузки связанных данных
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlite(ConfigurationHelper.DefaultConnection);
            });

            //Добавляем Identity систему, для работы с пользователями (авторизация, регистрация и т.д.)
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        });
}