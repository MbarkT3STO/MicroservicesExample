using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProductService.Database;
using ProductService.Database.Entities;
using ProductService.DTOs;

namespace ProductService.CQRS.Commands;

public class CreateProductCommand : IRequest<ProductGetDto>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductGetDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductGetDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ProductGetDto>(product);
    }
}