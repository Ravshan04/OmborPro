using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Inventory;
using OmborPro.Domain.Entities;

namespace OmborPro.Infrastructure.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IRepository<Warehouse> _repository;
    private readonly IMapper _mapper;

    public WarehouseService(IRepository<Warehouse> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WarehouseDto?> GetByIdAsync(Guid id)
    {
        var warehouse = await _repository.GetByIdAsync(id);
        return _mapper.Map<WarehouseDto>(warehouse);
    }

    public async Task<IEnumerable<WarehouseDto>> GetByOrganizationAsync(Guid organizationId)
    {
        var warehouses = await _repository.FindAsync(w => w.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
    }

    public async Task<WarehouseDto> CreateAsync(CreateWarehouseRequest request, Guid organizationId)
    {
        var warehouse = _mapper.Map<Warehouse>(request);
        warehouse.OrganizationId = organizationId;
        await _repository.AddAsync(warehouse);
        return _mapper.Map<WarehouseDto>(warehouse);
    }

    public async Task UpdateAsync(Guid id, UpdateWarehouseRequest request)
    {
        var warehouse = await _repository.GetByIdAsync(id);
        if (warehouse == null) throw new Exception("Warehouse not found");

        _mapper.Map(request, warehouse);
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
