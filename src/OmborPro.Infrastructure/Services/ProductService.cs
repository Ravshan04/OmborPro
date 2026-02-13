using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Product;
using OmborPro.Domain.Entities;

namespace OmborPro.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest request, Guid organizationId, Guid userId)
    {
        var product = new Product
        {
            OrganizationId = organizationId,
            Sku = request.Sku,
            Barcode = request.Barcode,
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId,
            Cost = request.Cost,
            SellingPrice = request.SellingPrice,
            CreatedBy = userId
        };

        await _productRepository.AddAsync(product);

        return MapToDto(product);
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product != null ? MapToDto(product) : null!;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByOrganizationAsync(Guid organizationId)
    {
        var products = await _productRepository.FindAsync(p => p.OrganizationId == organizationId);
        var dtos = new List<ProductDto>();
        foreach (var p in products) dtos.Add(MapToDto(p));
        return dtos;
    }

    public Task UpdateProductAsync(Guid id, CreateProductRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    private ProductDto MapToDto(Product product)
    {
        return new ProductDto(
            product.Id,
            product.Sku,
            product.Barcode,
            product.Name,
            product.Description,
            product.CategoryId,
            product.Unit.ToString(),
            product.Cost,
            product.SellingPrice,
            product.Weight,
            product.Length,
            product.Width,
            product.Height
        );
    }
}
