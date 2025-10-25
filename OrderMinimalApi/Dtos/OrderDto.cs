namespace OrderMinimalApi.Dtos
{
    public record OrderDto
    (
        int Id ,
        string FirstName ,
        string LastName ,
        string Status,
        DateTime OrderDate,
        decimal TotalAmount
    );
}
