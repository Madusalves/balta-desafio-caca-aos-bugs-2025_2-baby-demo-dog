namespace BugStore.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Customer? Customer { get; set; }
    public List<OrderLine> Lines { get; set; } = new List<OrderLine>();


    public decimal Total => Lines.Sum(line => line.Total);

    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(Product product, int quantity)
    {
        var line = new OrderLine
        {
            ProductId = product.Id,
            Quantity = quantity,
            UnitPrice = product.Price
        };

        Lines.Add(line);
        UpdatedAt = DateTime.UtcNow;
    }
}