using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.CQRS.Commands;
using CustomerService.CQRS.Queries;
using CustomerService.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public CustomersController(AppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _mediator.Send(new GetCustomersQuery());
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
    {
        var customer = await _mediator.Send(command);
        return Ok(customer);
    }
}
