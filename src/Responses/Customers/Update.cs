namespace BugStore.Responses.Customers;

public class UpdateCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string Message { get; set; } = "Customer updated successfully";
}