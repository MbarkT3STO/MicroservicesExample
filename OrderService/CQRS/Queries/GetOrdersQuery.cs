using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.DTOs;

namespace OrderService.CQRS.Queries;

public class GetOrdersQuery : IRequest<IEnumerable<OrderGetDto>>
{

}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderGetDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderGetDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.ToListAsync();
        return _mapper.Map<IEnumerable<OrderGetDto>>(orders);
    }
}