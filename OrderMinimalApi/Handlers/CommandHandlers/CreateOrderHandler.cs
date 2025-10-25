using FluentValidation;
using MediatR;
using OrderMinimalApi.Commands;
using OrderMinimalApi.Data;
using OrderMinimalApi.Dtos;
using OrderMinimalApi.Events;
using OrderMinimalApi.Models;

namespace OrderMinimalApi.Handlers.CommandHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IValidator<CreateOrderCommand> _validator;
        private readonly WriteDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateOrderHandler(IValidator<CreateOrderCommand> validator, WriteDbContext dbContext, IMediator mediator)
        {
            _validator = validator;
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var order = new Order
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Status = request.Status,
                TotalAmount = request.TotalAmount,
                OrderDate = DateTime.UtcNow
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var orderCreatedEvent = new OrderCreatedEvent
                (
                order.Id,
                order.FirstName,
                order.LastName,
                order.TotalAmount
                );
            
            //publish event to write in ReadDB & Must await => imp
             await _mediator.Publish(orderCreatedEvent,cancellationToken);

            var orderDto = new OrderDto
            (
                order.Id,
                 order.FirstName,
                order.LastName,
                 order.Status,                
                 order.OrderDate,
                 order.TotalAmount
            );
            return orderDto;

        }
    }
}
