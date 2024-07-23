using Cortside.AspNetCore.Common.Paging;
using rsr.Max.Dto;

namespace rsr.Max.Facade;

public interface ICustomerFacade
{
    Task<CustomerDto> CreateCustomerAsync(UpdateCustomerDto dto);
    Task<CustomerDto> GetCustomerAsync(Guid resourceId);
    Task<PagedList<CustomerDto>> SearchCustomersAsync(CustomerSearchDto search);
    Task<CustomerDto> UpdateCustomerAsync(Guid resourceId, UpdateCustomerDto dto);
    Task PublishCustomerStateChangedEventAsync(Guid resourceId);
}