namespace BugStore.Responses.Orders;

public class CreateOrdersResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
}

public class CreateItemOrdersResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}