namespace OrderMinimalApi.Dtos
{
    public record OrderSummeryDto(
        int Id,
        string CustomerName,
        string Status,
        decimal TotalAmount
    );


}
