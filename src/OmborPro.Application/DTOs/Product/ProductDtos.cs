using System;

namespace OmborPro.Application.DTOs.Product;

public record ProductDto(
    Guid Id,
    string Sku,
    string Barcode,
    string Name,
    string Description,
    Guid CategoryId,
    Guid? SupplierId,
    string Unit,
    decimal Quantity,
    decimal Cost,
    decimal SellingPrice,
    string? Location,
    decimal Weight,
    decimal Length,
    decimal Width,
    decimal Height
);

public record CreateProductRequest(
    string Sku,
    string Barcode,
    string Name,
    string Description,
    Guid CategoryId,
    Guid? SupplierId,
    string Unit,
    decimal Cost,
    decimal SellingPrice,
    string? Location
);

public record UpdateProductRequest(
    string Sku,
    string Barcode,
    string Name,
    string Description,
    Guid CategoryId,
    Guid? SupplierId,
    string Unit,
    decimal Cost,
    decimal SellingPrice,
    string? Location,
    decimal Weight,
    decimal Length,
    decimal Width,
    decimal Height
);
