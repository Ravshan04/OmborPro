using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Application.DTOs.Inventory;

namespace OmborPro.Application.Common.Interfaces;

public interface IInventoryService
{
    // Category methods
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request, Guid organizationId);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid organizationId);
    
    // Supplier methods
    Task<SupplierDto> CreateSupplierAsync(CreateSupplierRequest request, Guid organizationId);
    Task<IEnumerable<SupplierDto>> GetSuppliersAsync(Guid organizationId);
    
    // Customer methods
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request, Guid organizationId);
    Task<IEnumerable<CustomerDto>> GetCustomersAsync(Guid organizationId);
}
