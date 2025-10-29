namespace BugStore.Responses.Customers;

public class GetCustomerByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }

    public List<CustomerOrderResponse> RecentOrders { get; set; } = new(); //order list
}

public class CustomerOrderResponse
{
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}