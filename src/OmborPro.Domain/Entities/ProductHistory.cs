using System;
using OmborPro.Domain.Common;

namespace OmborPro.Domain.Entities;

public class ProductHistory : BaseEntity
{
    public Guid ProductId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public Guid? ChangedBy { get; set; }
}
