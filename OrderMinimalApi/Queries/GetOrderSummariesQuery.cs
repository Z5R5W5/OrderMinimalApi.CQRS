using MediatR;
using OrderMinimalApi.Dtos;

namespace OrderMinimalApi.Queries
{
    public class GetOrderSummariesQuery
    ():IRequest<List<OrderSummeryDto>>;
}
