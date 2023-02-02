using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Database;
using CustomerService.Database.Entities;
using CustomerService.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.CQRS.Queries;

public class GetCustomersQuery : IRequest<IEnumerable<CustomerGetDto>>
{

}

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerGetDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerGetDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers.ToListAsync(cancellationToken: cancellationToken);
        var customersDto = _mapper.Map<IEnumerable<CustomerGetDto>>(customers);
        return customersDto;
    }
}
