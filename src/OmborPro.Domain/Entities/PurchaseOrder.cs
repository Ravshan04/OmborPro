using System;
using System.Collections.Generic;
using OmborPro.Domain.Common;
using OmborPro.Domain.Enums;

namespace OmborPro.Domain.Entities;

public class PurchaseOrder : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public Guid? SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpectedDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public string? Notes { get; set; }
    public Guid? CreatedBy { get; set; }
}

public class OrderItem
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal ReceivedQuantity { get; set; }
    public decimal TotalPrice => Quantity * UnitCost;
}
