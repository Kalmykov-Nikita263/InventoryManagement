using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace InventoryManagement.Domain.ModelConfiguration;

/// <summary>
/// Класс-конфигурация для модели "Инвентаризация"
/// </summary>
public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        //Заполняем начальными данными
        builder.HasData(
            new Inventory
            {
                InventoryId = Guid.NewGuid(),
                InventoryNumber = "698754333121",
                Name = "Samsung Galaxy S22 8/256 Snapdragon 8 Gen 1",
                QuantityOnStock = 5,
                ActualQuantity = 5,
                AssetId = new Guid("01C8A12C-F97A-49BB-855B-12D4A8328190")
            },

            new Inventory
            {
                InventoryId = Guid.NewGuid(),
                InventoryNumber = "386129104576",
                Name = "Samsung Galaxy S23 8/128 Snapdragon 8 Gen 2",
                QuantityOnStock = 5,
                ActualQuantity = 5,
                AssetId = new Guid("43D1787F-EA86-4AAF-89AB-EEC3B936D22A")
            },

            new Inventory
            {
                InventoryId = Guid.NewGuid(),
                InventoryNumber = "499111487615",
                Name = "Samsung Galaxy S23 8/256 Snapdragon 8 Gen 2",
                QuantityOnStock = 5,
                ActualQuantity = 5,
                AssetId = new Guid("A29F676C-9DF5-4DFA-91E6-590436F83293")
            },

            new Inventory
            {
                InventoryId = Guid.NewGuid(),
                InventoryNumber = "332829934363",
                Name = "Samsung Galaxy S23 8/256 Snapdragon 8 Gen 2",
                QuantityOnStock = 5,
                ActualQuantity = 5,
                AssetId = new Guid("267055A3-9493-4D75-BE17-164F620F1DC7")
            }
        );
    }
}
