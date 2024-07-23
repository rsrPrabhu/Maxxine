using rsr.Max.Domain.Entities;

namespace rsr.Max.Data.Searches;

public interface ICustomerSearch: ISearch, ISearchBuilder<Customer> {
    Guid? CustomerResourceId { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
}