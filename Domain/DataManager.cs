using InventoryManagement.Domain.Repository.Abstractions;

namespace InventoryManagement.Domain;

/// <summary>
/// Класс, предназначенный для работы с данными из БД
/// </summary>
public class DataManager
{
    public IInventoryRepository Inventories { get; set; }

    public IAssetRepository Assets { get; set; }

    public DataManager(IInventoryRepository inventories, IAssetRepository assets)
    {
        Inventories = inventories;
        Assets = assets;
    }
}
