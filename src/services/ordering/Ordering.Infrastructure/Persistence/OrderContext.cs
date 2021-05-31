using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreateDate = DateTime.Now;
                        item.Entity.CretedBy = "masoud"; break;
                    case EntityState.Modified:
                        item.Entity.ModifiedDate = DateTime.Now;
                        item.Entity.ModifiedBy = "masoud"; break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
    //public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    //{
    //    public OrderContext CreateDbContext(string[] args)
    //    {
    //        var optionBuilder = new DbContextOptionsBuilder<OrderContext>();
    //        optionBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OrderDb;User Id=admin;Password=admin1234;Trusted_Connection=true;");
    //        return new OrderContext(optionBuilder.Options);
    //    }
    //}
}
