using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderMinimalApi.Data;
using OrderMinimalApi.Dtos;
using OrderMinimalApi.Queries;

namespace OrderMinimalApi.Handlers.QueriesHandlers
{
    public class GetOrderSummariesHandler : IRequestHandler<GetOrderSummariesQuery, List<OrderSummeryDto>>
    {
        private readonly ReadDbContext _dbContext;
        public GetOrderSummariesHandler(ReadDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<List<OrderSummeryDto>> Handle(GetOrderSummariesQuery request, CancellationToken cancellationToken)
        {
            var Orders =  await _dbContext.Orders.Select(
                o=>new OrderSummeryDto(
                    o.Id,
                    o.FirstName + " " +o.LastName,
                    o.Status,
                    o.TotalAmount
                    )
                ).ToListAsync();

            return Orders;
        }
    }
}
