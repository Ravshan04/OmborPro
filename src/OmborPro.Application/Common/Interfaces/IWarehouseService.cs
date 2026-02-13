using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Domain.Entities;

namespace OmborPro.Application.Common.Interfaces;

public interface IWarehouseService
{
    Task<Warehouse> GetByIdAsync(Guid id);
    Task<IEnumerable<Warehouse>> GetByOrganizationAsync(Guid organizationId);
    Task<Warehouse> CreateAsync(Warehouse warehouse);
    Task UpdateAsync(Warehouse warehouse);
    Task DeleteAsync(Guid id);
}
