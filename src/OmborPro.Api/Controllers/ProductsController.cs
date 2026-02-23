using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Product;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OmborPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orgIdString = User.FindFirst("OrganizationId")?.Value;
        if (orgIdString == null) return Unauthorized();
        
        var products = await _productService.GetProductsByOrganizationAsync(Guid.Parse(orgIdString));
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var orgIdString = User.FindFirst("OrganizationId")?.Value;
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (orgIdString == null || userIdString == null) return Unauthorized();
        
        var product = await _productService.CreateProductAsync(
            request, 
            Guid.Parse(orgIdString), 
            Guid.Parse(userIdString));
            
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        await _productService.UpdateProductAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
