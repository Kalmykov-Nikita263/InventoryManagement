using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Domain.Entities;

public class Inventory
{
    [Key]
    public Guid InventoryId { get; set; }

    [Required]
    public string InventoryNumber { get; set; }

    [Required]
    public string Name { get; set; }

    public int QuantityOnStock { get; set; }

    public int ActualQuantity { get; set; }

    //Навигационные свойства для EntityFramework
    public Guid AssetId { get; set; }

    public virtual Asset Asset { get; set; }
}
