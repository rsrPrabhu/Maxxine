using Cortside.AspNetCore.Common.Dtos;

namespace rsr.Max.Dto;

public class OrderItemDto: AuditableEntityDto {
    public int OrderItemId { get; set; }
    public Guid ItemId { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}