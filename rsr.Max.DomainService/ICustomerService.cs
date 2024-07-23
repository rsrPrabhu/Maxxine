using Cortside.AspNetCore.Common.Paging;
using rsr.Max.Data.Searches;
using rsr.Max.Domain.Entities;
using rsr.Max.Dto;

namespace rsr.Max.DomainService;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(UpdateCustomerDto dto);
    Task<Customer> GetCustomerAsync(Guid customerResourceId);
    Task<PagedList<Customer>> SearchCustomersAsync(CustomerSearch search);
    Task<Customer> UpdateCustomerAsync(Guid resourceId, UpdateCustomerDto dto);
    Task PublishCustomerStateChangedEventAsync(Guid resourceId);
}