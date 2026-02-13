using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OmborPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orgIdString = User.FindFirst("OrganizationId")?.Value;
        if (orgIdString == null) return Unauthorized();
        
        var warehouses = await _warehouseService.GetByOrganizationAsync(Guid.Parse(orgIdString));
        return Ok(warehouses);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Warehouse warehouse)
    {
        var orgIdString = User.FindFirst("OrganizationId")?.Value;
        if (orgIdString == null) return Unauthorized();
        
        warehouse.OrganizationId = Guid.Parse(orgIdString);
        var created = await _warehouseService.CreateAsync(warehouse);
        return Ok(created);
    }
}
