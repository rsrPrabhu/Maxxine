using rsr.Max.Domain.Entities;

namespace rsr.Max.Data.Searches;

public interface IOrderSearch: ISearch, ISearchBuilder<Order> {
    Guid? CustomerResourceId { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
}