using MediatR;
using OrderMinimalApi.Dtos;

namespace OrderMinimalApi.Queries
{
    public record GetOrderByIdQuery
    (int Id):IRequest<OrderDto?>;
    
}
