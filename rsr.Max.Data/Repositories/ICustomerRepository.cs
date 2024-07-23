using Cortside.AspNetCore.Common.Paging;
using rsr.Max.Data.Searches;
using rsr.Max.Domain.Entities;

namespace rsr.Max.Data.Repositories;

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);
    Task<Customer> GetAsync(Guid id);
    Task<PagedList<Customer>> SearchAsync(CustomerSearch model);
}