using Cortside.AspNetCore.Common.Dtos;
using rsr.Max.Domain.Entities;

namespace rsr.Max.Dto;

public class OrderDto : AuditableEntityDto {
    public int OrderId { get; set; }
    public Guid OrderResourceId { get; set; }
    public OrderStatus Status { get; set; }
    public CustomerDto Customer { get; set; }
    public AddressDto Address { get; set; }
    public List<OrderItemDto> Items { get; set; }
}