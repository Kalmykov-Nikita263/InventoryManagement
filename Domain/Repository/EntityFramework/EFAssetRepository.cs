using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Repository.EntityFramework;

public class EFAssetRepository : IAssetRepository
{

    private readonly ApplicationDbContext _context;

    public EFAssetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Asset> GetAllAssets()
        => _context.Assets.ToList();

    public Asset GetAssetById(Guid id)
        => _context.Assets.FirstOrDefault(x => x.AssetId == id);

    public void SaveAsset(Asset asset)
    {
        var existingAsset = _context.Assets.Find(asset.AssetId);

        if (existingAsset != null)
            _context.Entry(existingAsset).CurrentValues.SetValues(asset);
        
        else
            _context.Entry(asset).State = EntityState.Added;
        
        _context.SaveChanges();
    }

    public void DeleteAsset(Guid id)
    {
        var assetToDelete = _context.Assets.Find(id); 
        
        if (assetToDelete != null) 
        { 
            _context.Assets.Remove(assetToDelete); 
            _context.SaveChanges(); 
        } 
    }
}
