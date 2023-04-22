using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repository.Abstractions;
using System;
using System.Linq;

namespace InventoryManagement.Domain.Repository.EntityFramework;

public class EFInventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public EFInventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Inventory> GetAllInventories()
        => _context.Inventories;

    public Inventory GetInventoryById(Guid id)
        => _context.Inventories.FirstOrDefault(x => x.InventoryId == id);

    public void SaveInventory(Inventory inventory)
    {
        if (inventory.InventoryId == default)
            _context.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Added;

        else
            _context.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();
    }

    public void DeleteInventory(Guid id)
    {
        _context.Inventories.Remove(new Inventory { InventoryId = id });
        _context.SaveChanges();
    }
}
