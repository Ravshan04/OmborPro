using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Application.DTOs.Product;

namespace OmborPro.Application.Common.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductRequest request, Guid organizationId, Guid userId);
    Task<ProductDto> GetProductByIdAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetProductsByOrganizationAsync(Guid organizationId);
    Task UpdateProductAsync(Guid id, UpdateProductRequest request);
    Task DeleteProductAsync(Guid id);
}
