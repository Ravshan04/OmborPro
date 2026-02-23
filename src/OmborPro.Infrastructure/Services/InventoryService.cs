using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Inventory;
using OmborPro.Domain.Entities;

namespace OmborPro.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IRepository<Category> _categoryRepo;
    private readonly IRepository<Supplier> _supplierRepo;
    private readonly IRepository<Customer> _customerRepo;
    private readonly IMapper _mapper;

    public InventoryService(
        IRepository<Category> categoryRepo,
        IRepository<Supplier> supplierRepo,
        IRepository<Customer> customerRepo,
        IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _supplierRepo = supplierRepo;
        _customerRepo = customerRepo;
        _mapper = mapper;
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request, Guid organizationId)
    {
        var category = _mapper.Map<Category>(request);
        category.OrganizationId = organizationId;
        await _categoryRepo.AddAsync(category);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid organizationId)
    {
        var categories = await _categoryRepo.FindAsync(x => x.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierRequest request, Guid organizationId)
    {
        var supplier = _mapper.Map<Supplier>(request);
        supplier.OrganizationId = organizationId;
        await _supplierRepo.AddAsync(supplier);
        return _mapper.Map<SupplierDto>(supplier);
    }

    public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync(Guid organizationId)
    {
        var suppliers = await _supplierRepo.FindAsync(x => x.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request, Guid organizationId)
    {
        var customer = _mapper.Map<Customer>(request);
        customer.OrganizationId = organizationId;
        await _customerRepo.AddAsync(customer);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync(Guid organizationId)
    {
        var customers = await _customerRepo.FindAsync(x => x.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
}
