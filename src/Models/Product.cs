namespace BugStore.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Product()
    {
        Id = Guid.NewGuid();
    }

    public Product(string title, string description, decimal price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Slug = title.ToLower().Replace(" ", "-");
        Price = price;
    }
}