using InventoryManagement.Domain.Entities;
using System;
using System.Linq;

namespace InventoryManagement.Domain.Repository.Abstractions;

public interface IInventoryRepository
{
    IQueryable<Inventory> GetAllInventories();

    Inventory GetInventoryById(Guid id);

    void SaveInventory(Inventory inventory);

    void DeleteInventory(Guid id);
}
