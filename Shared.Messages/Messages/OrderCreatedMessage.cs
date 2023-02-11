using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Messages.Messages;

public class OrderCreatedMessage
{
    public int OrderId { get; set; }
    public string ProductId { get; set; }
    public string CustomerId { get; set; }
    public int Quantity { get; set; }
}
