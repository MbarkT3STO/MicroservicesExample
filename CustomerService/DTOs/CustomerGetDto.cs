using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.DTOs;

public class CustomerGetDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}
