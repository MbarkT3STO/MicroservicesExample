using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Database;
using ProductService.DTOs;

namespace ProductService.CQRS.Queries;

public class GetProductsQuery : IRequest<IEnumerable<ProductGetDto>>
{

}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductGetDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductGetDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken: cancellationToken);
        var productsDto = _mapper.Map<IEnumerable<ProductGetDto>>(products);
        return productsDto;
    }
}