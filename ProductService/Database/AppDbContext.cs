using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Database.Entities;

namespace ProductService.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Seed data

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = "P-1", Name = "Product 1", Description = "Product 1 Description", Price = 100 },
            new Product { Id = "P-2", Name = "Product 2", Description = "Product 2 Description", Price = 200 },
            new Product { Id = "P-3", Name = "Product 3", Description = "Product 3 Description", Price = 300 }
        );

        modelBuilder.Entity<Stock>().HasData(
            new Stock { Id = 1, ProductId = "P-1", Quantity = 100 },
            new Stock { Id = 2, ProductId = "P-2", Quantity = 100 },
            new Stock { Id = 3, ProductId = "P-3", Quantity = 100 }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
}
