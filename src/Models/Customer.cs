namespace BugStore.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();

    public Customer()
    {
        Id = Guid.NewGuid();
    }

    public Customer(string name, string email, string phone)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
    }
}