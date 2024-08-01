using Cortside.AspNetCore.Common.Paging;
using rsr.Max.Data.Searches;
using rsr.Max.Domain.Entities;
using rsr.Max.Dto;

namespace rsr.Max.DomainService;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Customer customer, CreateOrderDto dto);
    Task<Order> GetOrderAsync(Guid id);
    Task<PagedList<Order>> SearchOrdersAsync(OrderSearch search);
    Task PublishOrderStateChangedEventAsync(Guid id);
    Task<Order> UpdateOrderAsync(Guid id, UpdateOrderDto dto);
    Task<Order> AddOrderItemAsync(Guid id, OrderItemDto dto);
    Task<Order> SendNotificationAsync(Guid id);
}