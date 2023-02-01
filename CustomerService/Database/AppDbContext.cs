using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Seed data

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = "C-1", Name = "John Doe", City = "New York" },
            new Customer { Id = "C-2", Name = "Jane Doe", City = "Washington" },
            new Customer { Id = "C-3", Name = "John Smith", City = "Seattle" }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
}
