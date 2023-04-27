using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Repository.Abstractions;

/// <summary>
/// Интерфейс, который определяет CRUD-операции для модели "Имущество"
/// </summary>
public interface IAssetRepository
{
    IEnumerable<Asset> GetAllAssets();

    Asset GetAssetById(Guid id);

    void SaveAsset(Asset asset);

    void DeleteAsset(Guid id);
}
