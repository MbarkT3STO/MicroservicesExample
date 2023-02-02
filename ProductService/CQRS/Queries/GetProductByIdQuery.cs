using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProductService.Database;
using ProductService.DTOs;

namespace ProductService.CQRS.Queries;

public class GetProductByIdQuery : IRequest<ProductGetDto>
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductGetDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductGetDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
        return _mapper.Map<ProductGetDto>(product);
    }
}