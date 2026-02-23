using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmborPro.Application.DTOs.Orders;

namespace OmborPro.Application.Common.Interfaces;

public interface IOrderService
{
    // Purchase Orders
    Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request, Guid organizationId, Guid userId);
    Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersAsync(Guid organizationId);
    Task<PurchaseOrderDto?> GetPurchaseOrderByIdAsync(Guid id);

    // Sales Orders
    Task<SalesOrderDto> CreateSalesOrderAsync(CreateSalesOrderRequest request, Guid organizationId, Guid userId);
    Task<IEnumerable<SalesOrderDto>> GetSalesOrdersAsync(Guid organizationId);
    Task<SalesOrderDto?> GetSalesOrderByIdAsync(Guid id);
}
