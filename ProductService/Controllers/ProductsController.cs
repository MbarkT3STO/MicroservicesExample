using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.CQRS.Commands;
using ProductService.CQRS.Queries;
using ProductService.DTOs;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductGetDto>>> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductGetDto>> GetProductById(string id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductGetDto>> CreateProduct(CreateProductCommand command)
    {
        var product = await _mediator.Send(command);
        return Ok(product);
    }
}
