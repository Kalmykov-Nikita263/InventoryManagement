using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Repository.EntityFramework;

public class EFInventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public EFInventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Inventory> GetAllInventories()
        => _context.Inventories.ToList();

    public Inventory GetInventoryById(Guid id)
        => _context.Inventories.FirstOrDefault(x => x.InventoryId == id);

    public void SaveInventory(Inventory inventory)
    {
        var existingInventory = _context.Inventories.Find(inventory.InventoryId);

        if (existingInventory != null)
            _context.Entry(existingInventory).CurrentValues.SetValues(inventory);

        else
            _context.Entry(inventory).State = EntityState.Added;

        _context.SaveChanges();
    }

    public void DeleteInventory(Guid id)
    {
        var inventoryToDelete = _context.Inventories.Find(id);

        if (inventoryToDelete != null)
        {
            _context.Inventories.Remove(inventoryToDelete);
            _context.SaveChanges();
        }
    }
}
