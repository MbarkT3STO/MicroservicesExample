using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database.Entities;

public class Stock
{
    public int Id { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; }
}
