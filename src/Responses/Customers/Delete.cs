namespace BugStore.Responses.Customers;

public class DeleteCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Message { get; set; } = "Customer deleted successfully";
    public DateTime DeletedAt { get; set; }
}