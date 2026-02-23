using System;
using System.Collections.Generic;

namespace OmborPro.Application.DTOs.Orders;

public record OrderItemDto(
    Guid ProductId,
    decimal Quantity,
    decimal UnitCost,
    decimal ReceivedQuantity,
    decimal TotalPrice
);

public record CreateOrderItemRequest(
    Guid ProductId,
    decimal Quantity,
    decimal UnitCost
);

public record PurchaseOrderDto(
    Guid Id,
    string OrderNumber,
    Guid? SupplierId,
    Guid WarehouseId,
    string Status,
    List<OrderItemDto> Items,
    decimal TotalAmount,
    DateTime OrderDate,
    DateTime? ExpectedDate,
    DateTime? ReceivedDate,
    string? Notes
);

public record CreatePurchaseOrderRequest(
    Guid? SupplierId,
    Guid WarehouseId,
    List<CreateOrderItemRequest> Items,
    DateTime? ExpectedDate,
    string? Notes
);

public record SalesOrderDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    Guid WarehouseId,
    string Status,
    List<OrderItemDto> Items,
    decimal TotalAmount,
    DateTime OrderDate,
    DateTime? ShippedDate,
    DateTime? DeliveredDate,
    string? Notes
);

public record CreateSalesOrderRequest(
    Guid CustomerId,
    Guid WarehouseId,
    List<CreateOrderItemRequest> Items,
    string? Notes
);
