using System;

namespace OmborPro.Application.DTOs.Product;

public record ProductDto(
    Guid Id,
    string Sku,
    string Barcode,
    string Name,
    string Description,
    Guid CategoryId,
    string Unit,
    decimal Cost,
    decimal SellingPrice,
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
    string Unit,
    decimal Cost,
    decimal SellingPrice
);
