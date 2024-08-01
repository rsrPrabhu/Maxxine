using Cortside.AspNetCore.Common.Paging;
using rsr.Max.Dto;

namespace rsr.Max.Facade;

public interface IOrderFacade
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto input);
    Task<OrderDto> GetOrderAsync(Guid id);
    Task<PagedList<OrderDto>> SearchOrdersAsync(OrderSearchDto search);
    Task PublishOrderStateChangedEventAsync(Guid id);
    Task<OrderDto> UpdateOrderAsync(Guid id, UpdateOrderDto dto);
    Task<OrderDto> AddOrderItemAsync(Guid id, OrderItemDto dto);
    Task<OrderDto> SendNotificationAsync(Guid id);
}