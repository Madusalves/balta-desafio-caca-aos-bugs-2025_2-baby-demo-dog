namespace BugStore.Requests.Customers;

public class GetByIdCustomerRequest
{
    public bool IncludeOrders { get; set; } = false;
}