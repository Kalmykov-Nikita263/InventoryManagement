﻿using InventoryManagement.Domain.Repository.Abstractions;
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

public class ApplicationHostBuilder
{
    public static IHostBuilder CreateDefaultBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
        {
            hostContext.Configuration.Bind("ConnectionStrings", new ConfigurationHelper());
            
            services.AddTransient<IInventoryRepository, EFInventoryRepository>();
            services.AddTransient<IAssetRepository, EFAssetRepository>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<DataManager>();

            services.AddSingleton<AuthorizationWindow>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlite(ConfigurationHelper.DefaultConnection);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        });
}
