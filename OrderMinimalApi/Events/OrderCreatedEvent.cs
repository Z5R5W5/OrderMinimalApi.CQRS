using MediatR;

namespace OrderMinimalApi.Events
{
    public record OrderCreatedEvent(
        int OrderId,
        string FirstName,
        string LastName,
        decimal TotalCost
        ) : INotification
        ;
    
}
