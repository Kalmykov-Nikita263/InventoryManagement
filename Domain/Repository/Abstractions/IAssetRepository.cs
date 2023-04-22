using InventoryManagement.Domain.Entities;
using System.Linq;
using System;

namespace InventoryManagement.Domain.Repository.Abstractions;

public interface IAssetRepository
{
    IQueryable<Asset> GetAllAssets();

    Asset GetAssetById(Guid id);

    void SaveAsset(Asset asset);

    void DeleteAsset(Guid id);
}
