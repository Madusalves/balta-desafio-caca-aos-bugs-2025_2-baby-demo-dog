namespace BugStore.Models;

public class OrderLine
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;

    public Product? Product { get; set; }

    public OrderLine()
    {
        Id = Guid.NewGuid();
    }
}