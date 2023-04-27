using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Repository.EntityFramework;

/// <summary>
/// Класс, реализуйщий CRUD-операции для модели "Asset"
/// </summary>
public class EFAssetRepository : IAssetRepository
{
    private readonly ApplicationDbContext _context;

    public EFAssetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получаем список всего имущества
    /// </summary>
    /// <returns>Список со всеми записями об имуществе из БД</returns>
    public IEnumerable<Asset> GetAllAssets()
        => _context.Assets.ToList();

    /// <summary>
    /// Получаем конкретное имущество по его Id
    /// </summary>
    /// <param name="id">Идентификатор имущества</param>
    /// <returns>Найденное имущество из БД</returns>
    public Asset GetAssetById(Guid id)
        => _context.Assets.FirstOrDefault(x => x.AssetId == id);

    /// <summary>
    /// Метод, который отвечает за сохранение и изменение имущества
    /// </summary>
    /// <param name="asset">Имущество, которое следует сохранить/изменить</param>
    public void SaveAsset(Asset asset)
    {
        var existingAsset = _context.Assets.Find(asset.AssetId);

        if (existingAsset != null)
            _context.Entry(existingAsset).CurrentValues.SetValues(asset);
        
        else
            _context.Entry(asset).State = EntityState.Added;
        
        _context.SaveChanges();
    }

    /// <summary>
    /// Метод, который удаляет имущество из БД
    /// </summary>
    /// <param name="id">Идентификатор имущества, которое следует удалить</param>
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