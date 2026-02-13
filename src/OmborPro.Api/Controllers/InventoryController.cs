using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Domain.Enums;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OmborPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IStockService _stockService;

    public InventoryController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet("product/{productId}/warehouse/{warehouseId}")]
    public async Task<IActionResult> GetStock(Guid productId, Guid warehouseId)
    {
        var stock = await _stockService.GetAvailableStockAsync(productId, warehouseId);
        return Ok(new { ProductId = productId, WarehouseId = warehouseId, AvailableStock = stock });
    }

    [HttpPost("movement")]
    public async Task<IActionResult> RegisterMovement([FromBody] MovementRequest request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString == null) return Unauthorized();

        await _stockService.RegisterMovementAsync(
            request.ProductId,
            request.WarehouseId,
            request.Quantity,
            request.Type,
            request.RefType,
            request.RefId,
            Guid.Parse(userIdString),
            request.Notes
        );

        return Ok(new { Message = "Harakat muvaffaqiyatli saqlandi" });
    }
}

public record MovementRequest(
    Guid ProductId,
    Guid WarehouseId,
    decimal Quantity,
    MovementType Type,
    ReferenceType RefType,
    Guid RefId,
    string Notes = ""
);
