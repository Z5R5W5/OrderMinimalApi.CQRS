using MediatR;
using OrderMinimalApi.Dtos;

namespace OrderMinimalApi.Commands
{
    public record CreateOrderCommand(
        string FirstName,
        string LastName,
        string Status,
        decimal TotalAmount
    ):IRequest<OrderDto>;

}
