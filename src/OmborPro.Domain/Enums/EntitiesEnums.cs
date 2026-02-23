namespace OmborPro.Domain.Enums;

public enum SubscriptionType
{
    Free,
    Basic,
    Premium,
    Enterprise
}

public enum UnitType
{
    Piece,
    Kg,
    Liter,
    Box
}

public enum MovementType
{
    IN,
    OUT,
    TRANSFER,
    ADJUSTMENT,
    RETURN
}

public enum ReferenceType
{
    PurchaseOrder,
    SalesOrder,
    Adjustment,
    Transfer
}

public enum OrderStatus
{
    Draft,
    Pending,
    Approved,
    Confirmed,
    Packed,
    Shipped,
    Received,
    Delivered,
    Cancelled
}

public enum AuditAction
{
    CREATE,
    UPDATE,
    DELETE,
    VIEW
}

public enum CustomerStatus
{
    Active,
    Inactive
}

public enum NotificationType
{
    LowStock,
    OrderCreated,
    OrderApproved,
    OrderShipped,
    OrderReceived,
    OrderCancelled,
    System
}
