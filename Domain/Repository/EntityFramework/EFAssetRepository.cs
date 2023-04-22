using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repository.Abstractions;
using System;
using System.Linq;

namespace InventoryManagement.Domain.Repository.EntityFramework;

public class EFAssetRepository : IAssetRepository
{

    private readonly ApplicationDbContext _context;

    public EFAssetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Asset> GetAllAssets()
        => _context.Assets;

    public Asset GetAssetById(Guid id)
        => _context.Assets.FirstOrDefault(x => x.AssetId == id);

    public void SaveAsset(Asset asset)
    {
        if (asset.AssetId == default)
            _context.Entry(asset).State = Microsoft.EntityFrameworkCore.EntityState.Added;

        else
            _context.Entry(asset).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();
    }

    public void DeleteAsset(Guid id)
    {
        _context.Assets.Remove(new Asset { AssetId = id });
        _context.SaveChanges();
    }
}
