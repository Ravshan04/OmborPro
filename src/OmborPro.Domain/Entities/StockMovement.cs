using System;
using OmborPro.Domain.Common;
using OmborPro.Domain.Enums;

namespace OmborPro.Domain.Entities;

public class StockMovement : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public MovementType MovementType { get; set; }
    public decimal Quantity { get; set; }
    public ReferenceType ReferenceType { get; set; }
    public Guid ReferenceId { get; set; }
    public Guid? FromWarehouseId { get; set; }
    public Guid? ToWarehouseId { get; set; }
    public string Notes { get; set; } = string.Empty;
    public Guid PerformedBy { get; set; }
}
