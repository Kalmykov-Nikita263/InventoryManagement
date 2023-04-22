using InventoryManagement.Domain.Repository.Abstractions;
using InventoryManagement.Domain.Repository.EntityFramework;
using InventoryManagement.Domain;
using InventoryManagement.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryManagement.HostBuilders;

public class ApplicationHostBuilder
{
    public static IHostBuilder CreateDefaultBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
        {
            hostContext.Configuration.Bind("ConnectionStrings", new ConfigurationHelper());
            
            services.AddTransient<IInventoryRepository, EFInventoryRepository>();
            services.AddTransient<IAssetRepository, EFAssetRepository>();
            services.AddTransient<DataManager>();

            services.AddSingleton<MainWindow>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationHelper.DefaultConnection);
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        });
}
