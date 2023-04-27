using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Domain.ModelConfiguration;

/// <summary>
/// Класс-конфигурация для модели "Имущество"
/// </summary>
public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        //Настраиваем связь с моделью "Инвентаризация", с типом связи 1 к 1
        builder.HasOne(x => x.Inventory)
            .WithOne(x => x.Asset)
            .HasForeignKey<Inventory>(x => x.AssetId)
            .OnDelete(DeleteBehavior.Cascade);

        //Заполняем начальными данными
        builder.HasData(
            new Asset
            {
                AssetId = new System.Guid("01C8A12C-F97A-49BB-855B-12D4A8328190"),
                InventoryNumber = "698754333121",
                AssetType = Types.AssetType.Immovable,
                Name = "Samsung Galaxy S22 8/256 Snapdragon 8 Gen 1",
                Location = "г. Самара, Заводское шоссе, д. 10",
                Quantity = 5,
                Price = 59990
            },

            new Asset
            {
                AssetId = new System.Guid("43D1787F-EA86-4AAF-89AB-EEC3B936D22A"),
                InventoryNumber = "386129104576",
                AssetType = Types.AssetType.Immovable,
                Name = "Samsung Galaxy S22 8/128 Snapdragon 8 Gen 1",
                Location = "г. Самара, Заводское шоссе, д. 10",
                Quantity = 5,
                Price = 54990
            },

            new Asset
            { 
                AssetId = new System.Guid("A29F676C-9DF5-4DFA-91E6-590436F83293"),
                InventoryNumber = "499111487615",
                AssetType = Types.AssetType.Immovable,
                Name = "Samsung Galaxy S23 8/128 Snapdragon 8 Gen 2",
                Location = "г. Самара, Заводское шоссе, д. 10",
                Quantity = 5,
                Price = 74990
            },

            new Asset
            {
                AssetId = new System.Guid("267055A3-9493-4D75-BE17-164F620F1DC7"),
                InventoryNumber = "332829934363",
                AssetType = Types.AssetType.Immovable,
                Name = "Samsung Galaxy S23 8/256 Snapdragon 8 Gen 2",
                Location = "г. Самара, Заводское шоссе, д. 10",
                Quantity = 5,
                Price = 79990
            }
        );
    }
}
