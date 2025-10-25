namespace OrderMinimalApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } 
        public required string LastName { get; set; }
        public required string Status { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
