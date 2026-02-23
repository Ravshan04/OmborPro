using System;
using OmborPro.Domain.Common;
using OmborPro.Domain.Enums;

namespace OmborPro.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;
    
    // Summary fields
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
}
