using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OrderService.Database;
using OrderService.DTOs;

namespace OrderService.CQRS.Queries;

public class GetOrderByIdQuery : IRequest<OrderGetDto>
{
    public string Id { get; set; }

    public GetOrderByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderGetDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderGetDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        return _mapper.Map<OrderGetDto>(order);
    }
}