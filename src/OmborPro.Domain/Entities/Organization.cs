using System;
using OmborPro.Domain.Common;
using OmborPro.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace OmborPro.Domain.Entities;

public class Organization : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public SubscriptionType SubscriptionType { get; set; }
    public string Currency { get; set; } = "USD";
    public string Timezone { get; set; } = "UTC";
    public int LowStockThreshold { get; set; }
    public bool IsActive { get; set; } = true;
}
