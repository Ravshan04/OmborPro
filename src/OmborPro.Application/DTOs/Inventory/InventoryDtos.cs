using System;

namespace OmborPro.Application.DTOs.Inventory;

public record CategoryDto(
    Guid Id,
    string Name,
    string? Description,
    Guid? ParentId,
    int ProductCount,
    decimal TotalValue
);

public record CreateCategoryRequest(
    string Name,
    string? Description,
    Guid? ParentId
);

public record SupplierDto(
    Guid Id,
    string Name,
    string? ContactPerson,
    string? Email,
    string? Phone,
    string? Address,
    double Rating,
    int LeadTime,
    int ProductCount
);

public record CreateSupplierRequest(
    string Name,
    string? ContactPerson,
    string? Email,
    string? Phone,
    string? Address,
    int LeadTime
);

public record CustomerDto(
    Guid Id,
    string Name,
    string? Email,
    string? Phone,
    string? Address,
    string Status,
    int TotalOrders,
    decimal TotalSpent
);

public record CreateCustomerRequest(
    string Name,
    string? Email,
    string? Phone,
    string? Address
);
