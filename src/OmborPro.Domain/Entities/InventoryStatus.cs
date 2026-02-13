using System;
using OmborPro.Domain.Common;

namespace OmborPro.Domain.Entities;

public class InventoryStatus : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal Quantity { get; set; }
    public decimal ReservedQuantity { get; set; }
    public decimal AvailableQuantity => Quantity - ReservedQuantity;
    public DateTime LastStockUpdate { get; set; } = DateTime.UtcNow;
}
