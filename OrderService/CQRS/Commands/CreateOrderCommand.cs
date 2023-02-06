using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OrderService.Database;
using OrderService.Database.Entities;
using OrderService.DTOs;

namespace OrderService.CQRS.Commands;

public class CreateOrderCommand : IRequest<OrderGetDto>
{
    public DateTime OrderDate { get; set; }
    public string ProductId { get; set; }
    public string CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderGetDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderGetDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request);

        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _dbContext.Entry(order).ReloadAsync(cancellationToken);

        return _mapper.Map<OrderGetDto>(order);
    }
}

