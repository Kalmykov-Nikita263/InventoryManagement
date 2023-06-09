﻿using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Repository.EntityFramework;

/// <summary>
/// Класс, реализуйщий CRUD-операции для модели "Inventory"
/// </summary>
public class EFInventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public EFInventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получаем список всех записей инвентаризации
    /// </summary>
    /// <returns>Список со всеми записями об инвентаризации из БД</returns>
    public IEnumerable<Inventory> GetAllInventories()
        => _context.Inventories.ToList();

    /// <summary>
    /// Получаем запись инвентаризации по Id
    /// </summary>
    /// <param name="id">Идентификатор инвентаризации</param>
    /// <returns>Найденная запись об инвентаризации из БД</returns>
    public Inventory GetInventoryById(Guid id)
        => _context.Inventories.FirstOrDefault(x => x.InventoryId == id);


    /// <summary>
    /// Метод, который отвечает за сохранение и изменение инвентаризации
    /// </summary>
    /// <param name="asset">Инвентаризация, которую следует сохранить/изменить</param>
    public void SaveInventory(Inventory inventory)
    {
        var existingInventory = _context.Inventories.Find(inventory.InventoryId);

        if (existingInventory != null)
            _context.Entry(existingInventory).CurrentValues.SetValues(inventory);

        else
            _context.Entry(inventory).State = EntityState.Added;

        _context.SaveChanges();
    }

    /// <summary>
    /// Метод, который удаляет инвентаризацию из БД
    /// </summary>
    /// <param name="id">Идентификатор инвентаризации, которую следует удалить</param>
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
