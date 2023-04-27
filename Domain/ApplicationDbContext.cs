using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.ModelConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Domain;

/// <summary>
/// Класс-контекст приложения
/// </summary>
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    //Отдаём конфигурацию, в том числе и строку для подключения в конструктор родительского класса
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    //Будущие таблицы в БД
    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Применяем конфигурации моделей
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());

        builder.ApplyConfiguration(new InventoryConfiguration());
        builder.ApplyConfiguration(new AssetConfiguration());
    }
}
