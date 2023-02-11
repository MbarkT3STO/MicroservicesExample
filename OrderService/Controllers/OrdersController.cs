using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderService.Configs;
using OrderService.CQRS.Commands;
using OrderService.CQRS.Queries;
using Shared.Messages.Messages;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOptions<RabbitMqEndPointsConfig> _rabbitMqEndPointsConfig;
    private readonly IBus _bus;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(IMediator mediator, IOptions<RabbitMqEndPointsConfig> rabbitMqEndPointsConfig, IBus bus, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _rabbitMqEndPointsConfig = rabbitMqEndPointsConfig;
        _bus = bus;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _mediator.Send(new GetOrdersQuery());

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var order = await _mediator.Send(command);

        // Publish message to RabbitMQ
        var message = new OrderCreatedMessage
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
        };

        await _publishEndpoint.Publish(message);

        return Ok(order);
    }

}
