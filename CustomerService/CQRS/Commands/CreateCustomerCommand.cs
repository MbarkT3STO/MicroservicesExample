using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Database;
using CustomerService.Database.Entities;
using CustomerService.DTOs;
using MediatR;

namespace CustomerService.CQRS.Commands;

public class CreateCustomerCommand : IRequest<CustomerGetDto>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerGetDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerGetDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request);
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CustomerGetDto>(customer);
    }
}