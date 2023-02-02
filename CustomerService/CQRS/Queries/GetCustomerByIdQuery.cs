using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Database;
using CustomerService.DTOs;
using MediatR;

namespace CustomerService.CQRS.Queries;

public class GetCustomerByIdQuery : IRequest<CustomerGetDto>
{
    public string Id { get; set; }

    public GetCustomerByIdQuery(string id)
    {
        Id = id;
    }
}


public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerGetDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerGetDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.Id);
        return _mapper.Map<CustomerGetDto>(customer);
    }
}