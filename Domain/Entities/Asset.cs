using System;
using System.ComponentModel.DataAnnotations;
using InventoryManagement.Domain.Types;

namespace InventoryManagement.Domain.Entities;

/// <summary>
/// Класс, который представляет модель "Имущество"
/// </summary>
public class Asset
{
    [Key]
    public Guid AssetId { get; set; }

    [Required]
    [MinLength(12, ErrorMessage = "Строка должна содержать 12 символов")]
    [MaxLength(12, ErrorMessage = "Строка должна содержать 12 символов")]
    public string InventoryNumber { get; set; }

    //Тип имущества Движимое/Недвижимое
    [Required]
    public AssetType AssetType { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public double Price { get; set; }

    //Навигационное свойство EntityFramework
    public virtual Inventory Inventory { get; set; }
}