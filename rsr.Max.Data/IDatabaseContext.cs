using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using rsr.Max.Domain.Entities;

namespace rsr.Max.Data;

public interface IDatabaseContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }

    void RemoveRange(IEnumerable<object> entities);
    EntityEntry Remove(object entity);
}