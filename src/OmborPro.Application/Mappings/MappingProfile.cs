using AutoMapper;
using OmborPro.Application.DTOs.Auth;
using OmborPro.Application.DTOs.Inventory;
using OmborPro.Application.DTOs.Orders;
using OmborPro.Application.DTOs.Product;
using OmborPro.Domain.Entities;

namespace OmborPro.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();

        // Profile/User mappings
        CreateMap<User, ProfileDto>();
        CreateMap<UpdateProfileRequest, User>();

        // Category mappings
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryRequest, Category>();

        // Supplier mappings
        CreateMap<Supplier, SupplierDto>();
        CreateMap<CreateSupplierRequest, Supplier>();

        // Warehouse mappings
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<CreateWarehouseRequest, Warehouse>();
        CreateMap<UpdateWarehouseRequest, Warehouse>();

        // Customer mappings
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerRequest, Customer>();

        // Order mappings
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CreateOrderItemRequest, OrderItem>();
        
        CreateMap<PurchaseOrder, PurchaseOrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreatePurchaseOrderRequest, PurchaseOrder>();

        CreateMap<SalesOrder, SalesOrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateSalesOrderRequest, SalesOrder>();
        
        // Notification mappings
        // (Will add if needed)
    }
}
