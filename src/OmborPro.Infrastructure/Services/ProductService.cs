using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Product;
using OmborPro.Domain.Entities;

namespace OmborPro.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest request, Guid organizationId, Guid userId)
    {
        var product = _mapper.Map<Product>(request);
        product.OrganizationId = organizationId;
        product.CreatedBy = userId;

        await _productRepository.AddAsync(product);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByOrganizationAsync(Guid organizationId)
    {
        var products = await _productRepository.FindAsync(p => p.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) throw new Exception("Product not found");

        _mapper.Map(request, product);
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            await _productRepository.DeleteAsync(product);
        }
    }
}
