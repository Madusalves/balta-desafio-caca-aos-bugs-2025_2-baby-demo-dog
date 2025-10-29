namespace BugStore.Requests.Orders;

public class CreateOrdersRequest
{
    public Guid CustomerId { get; set; }
    public List<CreateItemOrdersRequest> Items { get; set; } = new List<CreateItemOrdersRequest>();
}

public class CreateItemOrdersRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}