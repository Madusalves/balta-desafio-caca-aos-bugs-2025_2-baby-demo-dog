namespace BugStore.Responses.Customers;

public class CreateCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Message { get; set; } = "Customer created successfully";
}