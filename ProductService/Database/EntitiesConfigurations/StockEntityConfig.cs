using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Database.Entities;

namespace ProductService.Database.EntitiesConfigurations;

public class StockEntityConfig : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne(x => x.Product).WithOne(x => x.Stock);
    }
}
