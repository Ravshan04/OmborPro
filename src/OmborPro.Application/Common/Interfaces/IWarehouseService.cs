using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Application.DTOs.Inventory;

namespace OmborPro.Application.Common.Interfaces;

public interface IWarehouseService
{
    Task<WarehouseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<WarehouseDto>> GetByOrganizationAsync(Guid organizationId);
    Task<WarehouseDto> CreateAsync(CreateWarehouseRequest request, Guid organizationId);
    Task UpdateAsync(Guid id, UpdateWarehouseRequest request);
    Task DeleteAsync(Guid id);
}
