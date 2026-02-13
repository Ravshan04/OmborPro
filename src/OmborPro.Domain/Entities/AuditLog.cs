using System;
using OmborPro.Domain.Common;
using OmborPro.Domain.Enums;

namespace OmborPro.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid UserId { get; set; }
    public AuditAction Action { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public Guid EntityId { get; set; }
    public string ChangesBefore { get; set; } = string.Empty; // JSON string
    public string ChangesAfter { get; set; } = string.Empty; // JSON string
    public string IpAddress { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
