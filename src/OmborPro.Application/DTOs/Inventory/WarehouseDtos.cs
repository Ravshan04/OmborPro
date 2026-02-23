using System;

namespace OmborPro.Application.DTOs.Inventory;

public record WarehouseDto(
    Guid Id,
    Guid OrganizationId,
    string Name,
    string Code,
    string? Location
);

public record CreateWarehouseRequest(
    string Name,
    string Code,
    string? Location
);

public record UpdateWarehouseRequest(
    string Name,
    string Code,
    string? Location
);
