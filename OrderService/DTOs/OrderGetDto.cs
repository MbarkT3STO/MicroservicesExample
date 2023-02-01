using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.DTOs;

public class OrderGetDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string ProductId { get; set; }
    public string CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}
