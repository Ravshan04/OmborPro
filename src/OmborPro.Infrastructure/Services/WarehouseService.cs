using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Domain.Entities;

namespace OmborPro.Infrastructure.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IRepository<Warehouse> _repository;

    public WarehouseService(IRepository<Warehouse> repository)
    {
        _repository = repository;
    }

    public async Task<Warehouse> GetByIdAsync(Guid id)
    {
        return (await _repository.GetByIdAsync(id))!;
    }

    public async Task<IEnumerable<Warehouse>> GetByOrganizationAsync(Guid organizationId)
    {
        return await _repository.FindAsync(w => w.OrganizationId == organizationId);
    }

    public async Task<Warehouse> CreateAsync(Warehouse warehouse)
    {
        await _repository.AddAsync(warehouse);
        return warehouse;
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
        await _repository.UpdateAsync(warehouse);
    }

    public async Task DeleteAsync(Guid id)
    {
        var warehouse = await _repository.GetByIdAsync(id);
        if (warehouse != null)
        {
            await _repository.DeleteAsync(warehouse);
        }
    }
}
