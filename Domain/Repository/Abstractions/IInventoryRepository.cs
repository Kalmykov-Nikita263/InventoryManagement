using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Repository.Abstractions;

/// <summary>
/// Интерфейс, который определяет CRUD-операции для модели "Инвентаризация"
/// </summary>
public interface IInventoryRepository
{
    IEnumerable<Inventory> GetAllInventories();

    Inventory GetInventoryById(Guid id);

    void SaveInventory(Inventory inventory);

    void DeleteInventory(Guid id);
}
