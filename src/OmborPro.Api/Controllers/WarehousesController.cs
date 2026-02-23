using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Inventory;
using System.Security.Claims;

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

    private Guid GetOrgId()
    {
        var orgIdString = User.FindFirst("OrganizationId")?.Value;
        return orgIdString != null ? Guid.Parse(orgIdString) : Guid.Empty;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _warehouseService.GetByOrganizationAsync(orgId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _warehouseService.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseRequest request)
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _warehouseService.CreateAsync(request, orgId);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseRequest request)
    {
        await _warehouseService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _warehouseService.DeleteAsync(id);
        return NoContent();
    }
}
