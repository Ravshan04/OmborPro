using System;
using System.Collections.Generic;
using OmborPro.Domain.Common;
using OmborPro.Domain.Enums;

namespace OmborPro.Domain.Entities;

public class Product : BaseEntity
{
    public string Sku { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid? SupplierId { get; set; }
    public UnitType Unit { get; set; }
    public decimal Quantity { get; set; } // Current stock level
    public decimal ReorderPoint { get; set; }
    public decimal ReorderQuantity { get; set; }
    public decimal Cost { get; set; }
    public decimal SellingPrice { get; set; }
    public string? Location { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Dimensions
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    
    public List<string> Images { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public Guid CreatedBy { get; set; }
}
