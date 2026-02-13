using System;
using System.Linq;
using System.Threading.Tasks;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Domain.Entities;
using OmborPro.Domain.Enums;

namespace OmborPro.Infrastructure.Services;

public class StockService : IStockService
{
    private readonly IRepository<StockMovement> _movementRepository;
    private readonly IRepository<InventoryStatus> _inventoryRepository;

    public StockService(
        IRepository<StockMovement> movementRepository, 
        IRepository<InventoryStatus> inventoryRepository)
    {
        _movementRepository = movementRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task RegisterMovementAsync(
        Guid productId, 
        Guid warehouseId, 
        decimal quantity, 
        MovementType type, 
        ReferenceType refType, 
        Guid refId, 
        Guid userId,
        string notes = "")
    {
        // 1. Get OrganizationId (needed for isolation)
        // In a real app, this should be validated or passed from context
        // Here we assume organizationId is linked to the warehouse or product
        
        // 2. Record Movement
        var movement = new StockMovement
        {
            ProductId = productId,
            WarehouseId = warehouseId,
            MovementType = type,
            Quantity = quantity,
            ReferenceType = refType,
            ReferenceId = refId,
            PerformedBy = userId,
            Notes = notes
            // OrganizationId should be set here too
        };
        
        await _movementRepository.AddAsync(movement);

        // 3. Update Inventory Status
        var inventoryList = await _inventoryRepository.FindAsync(i => 
            i.ProductId == productId && i.WarehouseId == warehouseId);
        
        var inventory = inventoryList.FirstOrDefault();

        if (inventory == null)
        {
            inventory = new InventoryStatus
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                Quantity = 0,
                ReservedQuantity = 0
            };
            await _inventoryRepository.AddAsync(inventory);
        }

        // Adjust quantity based on type
        if (type == MovementType.IN || type == MovementType.RETURN)
        {
            inventory.Quantity += quantity;
        }
        else if (type == MovementType.OUT)
        {
            inventory.Quantity -= quantity;
        }
        else if (type == MovementType.ADJUSTMENT)
        {
            // For adjustment, quantity can be positive or negative
            inventory.Quantity += quantity;
        }

        inventory.LastStockUpdate = DateTime.UtcNow;
        await _inventoryRepository.UpdateAsync(inventory);
    }

    public async Task<decimal> GetAvailableStockAsync(Guid productId, Guid warehouseId)
    {
        var inventoryList = await _inventoryRepository.FindAsync(i => 
            i.ProductId == productId && i.WarehouseId == warehouseId);
        
        var inventory = inventoryList.FirstOrDefault();
        return inventory?.AvailableQuantity ?? 0;
    }
}
