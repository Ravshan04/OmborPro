using System;
using System.Threading.Tasks;
using OmborPro.Domain.Enums;

namespace OmborPro.Application.Common.Interfaces;

public interface IStockService
{
    Task RegisterMovementAsync(
        Guid productId, 
        Guid warehouseId, 
        decimal quantity, 
        MovementType type, 
        ReferenceType refType, 
        Guid refId, 
        Guid userId,
        string notes = "");
    
    Task<decimal> GetAvailableStockAsync(Guid productId, Guid warehouseId);
}
