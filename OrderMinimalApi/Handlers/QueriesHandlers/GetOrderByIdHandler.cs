using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderMinimalApi.Data;
using OrderMinimalApi.Dtos;
using OrderMinimalApi.Queries;

namespace OrderMinimalApi.Handlers.QueriesHandlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly ReadDbContext _dbContext;
        public GetOrderByIdHandler(ReadDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == request.Id);

            if (order == null)
                return null;

            return new OrderDto(
                order.Id,
                order.FirstName,
                order.LastName,
                order.Status,
                order.OrderDate,
                order.TotalAmount
            );
        }
    }
}
