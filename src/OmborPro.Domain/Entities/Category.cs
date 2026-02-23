using System;
using OmborPro.Domain.Common;

namespace OmborPro.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    
    // Computed or cached fields
    public int ProductCount { get; set; }
    public decimal TotalValue { get; set; }
}
