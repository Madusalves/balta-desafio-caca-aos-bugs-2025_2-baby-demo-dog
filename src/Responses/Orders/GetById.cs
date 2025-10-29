namespace BugStore.Responses.Orders;

public class GetOrdersByIdResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal Total { get; set; }
}