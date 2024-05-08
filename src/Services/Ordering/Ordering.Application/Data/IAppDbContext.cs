using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using System.Collections.Generic;

namespace Ordering.Application.Data
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
