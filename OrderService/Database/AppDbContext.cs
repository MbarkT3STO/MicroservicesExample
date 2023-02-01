using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Database.Entities;

namespace OrderService.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Seed data
        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                ProductId = "P-1",
                CustomerId = "C-1",
                Quantity = 20,
                Price = 100,
                Total = 2000
            },
            new Order
            {
                Id = 2,
                OrderDate = DateTime.Now,
                ProductId = "P-2",
                CustomerId = "C-2",
                Quantity = 15,
                Price = 30,
                Total = 450
            },
            new Order
            {
                Id = 3,
                OrderDate = DateTime.Now,
                ProductId = "P-1",
                CustomerId = "C-3",
                Quantity = 10,
                Price = 50,
                Total = 500
            }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Order> Orders { get; set; }
}
