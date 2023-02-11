using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Database;

namespace ProductService.CQRS.Commands;

public class DecreaseStockCommand : IRequest
{
    public string ProductId { get; set; }
    public int QuantityToDecrease { get; set; }

    public DecreaseStockCommand(string productId, int quantityToDecrease)
    {
        ProductId = productId;
        QuantityToDecrease = quantityToDecrease;
    }
}

public class DecreaseStockCommandHandler : IRequestHandler<DecreaseStockCommand>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DecreaseStockCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == request.ProductId, cancellationToken: cancellationToken);

        stock.Quantity -= request.QuantityToDecrease;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}