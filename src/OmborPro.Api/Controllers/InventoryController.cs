using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Inventory;
using System.Security.Claims;

namespace OmborPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    private Guid GetOrgId()
    {
        var orgIdString = User.FindFirst("OrganizationId")?.Value;
        return orgIdString != null ? Guid.Parse(orgIdString) : Guid.Empty;
    }

    // Categories
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _inventoryService.GetCategoriesAsync(orgId);
        return Ok(result);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _inventoryService.CreateCategoryAsync(request, orgId);
        return Ok(result);
    }

    // Suppliers
    [HttpGet("suppliers")]
    public async Task<IActionResult> GetSuppliers()
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _inventoryService.GetSuppliersAsync(orgId);
        return Ok(result);
    }

    [HttpPost("suppliers")]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierRequest request)
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _inventoryService.CreateSupplierAsync(request, orgId);
        return Ok(result);
    }

    // Customers
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _inventoryService.GetCustomersAsync(orgId);
        return Ok(result);
    }

    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        var orgId = GetOrgId();
        if (orgId == Guid.Empty) return Unauthorized();
        var result = await _inventoryService.CreateCustomerAsync(request, orgId);
        return Ok(result);
    }
}
