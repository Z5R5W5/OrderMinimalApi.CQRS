using MediatR;
using OrderMinimalApi.Data;
using OrderMinimalApi.Events;
using OrderMinimalApi.Models;

namespace OrderMinimalApi.Projections.CreatedProjectionsHandlers
{
    public class CreatedOrderProjectionHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ReadDbContext _dbContext;
        public CreatedOrderProjectionHandler(ReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = notification.OrderId,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Status = "Created",
                OrderDate = DateTime.Now,
                TotalAmount = notification.TotalCost
            };
            await _dbContext.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
