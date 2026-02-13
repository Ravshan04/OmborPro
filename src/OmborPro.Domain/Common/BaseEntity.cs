using System;
using MongoDB.Bson.Serialization.Attributes;

namespace OmborPro.Domain.Common;

public abstract class BaseEntity
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid OrganizationId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted => DeletedAt.HasValue;
}
