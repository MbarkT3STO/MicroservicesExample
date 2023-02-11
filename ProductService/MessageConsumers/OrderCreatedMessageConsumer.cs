using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using ProductService.CQRS.Commands;
using Shared.Messages.Messages;

namespace ProductService.MessageConsumers;

public class OrderCreatedMessageConsumer : IConsumer<OrderCreatedMessage>
{
    private readonly IMediator _mediator;

    public OrderCreatedMessageConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
    {
        var message = context.Message;

        await _mediator.Send(new DecreaseStockCommand(message.ProductId, message.Quantity));
    }
}
