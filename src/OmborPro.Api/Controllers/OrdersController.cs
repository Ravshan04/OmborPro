using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Orders;
using System.Security.Claims;

namespace OmborPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    private (Guid orgId, Guid userId) GetUserInfo()
    {
        var orgId = Guid.Parse(User.FindFirst("OrganizationId")?.Value ?? Guid.Empty.ToString());
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
        return (orgId, userId);
    }

    // Purchase Orders
    [HttpGet("purchase")]
    public async Task<IActionResult> GetPurchaseOrders()
    {
        var (orgId, _) = GetUserInfo();
        var result = await _orderService.GetPurchaseOrdersAsync(orgId);
        return Ok(result);
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderRequest request)
    {
        var (orgId, userId) = GetUserInfo();
        var result = await _orderService.CreatePurchaseOrderAsync(request, orgId, userId);
        return Ok(result);
    }

    // Sales Orders
    [HttpGet("sales")]
    public async Task<IActionResult> GetSalesOrders()
    {
        var (orgId, _) = GetUserInfo();
        var result = await _orderService.GetSalesOrdersAsync(orgId);
        return Ok(result);
    }

    [HttpPost("sales")]
    public async Task<IActionResult> CreateSalesOrder([FromBody] CreateSalesOrderRequest request)
    {
        var (orgId, userId) = GetUserInfo();
        var result = await _orderService.CreateSalesOrderAsync(request, orgId, userId);
        return Ok(result);
    }
}
