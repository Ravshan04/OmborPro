using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Orders;
using OmborPro.Domain.Entities;
using OmborPro.Domain.Enums;

namespace OmborPro.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<PurchaseOrder> _purchaseOrderRepo;
    private readonly IRepository<SalesOrder> _salesOrderRepo;
    private readonly IMapper _mapper;

    public OrderService(
        IRepository<PurchaseOrder> purchaseOrderRepo,
        IRepository<SalesOrder> salesOrderRepo,
        IMapper mapper)
    {
        _purchaseOrderRepo = purchaseOrderRepo;
        _salesOrderRepo = salesOrderRepo;
        _mapper = mapper;
    }

    public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request, Guid organizationId, Guid userId)
    {
        var order = _mapper.Map<PurchaseOrder>(request);
        order.OrganizationId = organizationId;
        order.CreatedBy = userId;
        order.Status = OrderStatus.Pending;
        order.OrderNumber = "PO-" + DateTime.UtcNow.Ticks.ToString().Substring(10);
        
        // Calculate Total
        order.TotalAmount = 0;
        foreach(var item in order.Items)
        {
            order.TotalAmount += item.Quantity * item.UnitCost;
        }

        await _purchaseOrderRepo.AddAsync(order);
        return _mapper.Map<PurchaseOrderDto>(order);
    }

    public async Task<IEnumerable<PurchaseOrderDto>> GetPurchaseOrdersAsync(Guid organizationId)
    {
        var orders = await _purchaseOrderRepo.FindAsync(x => x.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<PurchaseOrderDto>>(orders);
    }

    public async Task<PurchaseOrderDto?> GetPurchaseOrderByIdAsync(Guid id)
    {
        var order = await _purchaseOrderRepo.GetByIdAsync(id);
        return _mapper.Map<PurchaseOrderDto>(order);
    }

    public async Task<SalesOrderDto> CreateSalesOrderAsync(CreateSalesOrderRequest request, Guid organizationId, Guid userId)
    {
        var order = _mapper.Map<SalesOrder>(request);
        order.OrganizationId = organizationId;
        order.CreatedBy = userId;
        order.Status = OrderStatus.Pending;
        order.OrderNumber = "SO-" + DateTime.UtcNow.Ticks.ToString().Substring(10);

        // Calculate Total
        order.TotalAmount = 0;
        foreach (var item in order.Items)
        {
            order.TotalAmount += item.Quantity * item.UnitCost;
        }

        await _salesOrderRepo.AddAsync(order);
        return _mapper.Map<SalesOrderDto>(order);
    }

    public async Task<IEnumerable<SalesOrderDto>> GetSalesOrdersAsync(Guid organizationId)
    {
        var orders = await _salesOrderRepo.FindAsync(x => x.OrganizationId == organizationId);
        return _mapper.Map<IEnumerable<SalesOrderDto>>(orders);
    }

    public async Task<SalesOrderDto?> GetSalesOrderByIdAsync(Guid id)
    {
        var order = await _salesOrderRepo.GetByIdAsync(id);
        return _mapper.Map<SalesOrderDto>(order);
    }
}
