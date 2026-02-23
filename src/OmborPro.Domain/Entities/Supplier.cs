using System;
using OmborPro.Domain.Common;

namespace OmborPro.Domain.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public double Rating { get; set; }
    public int LeadTime { get; set; } // in days
    public int ProductCount { get; set; }
}
