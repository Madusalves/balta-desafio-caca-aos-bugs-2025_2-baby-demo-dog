namespace BugStore.Responses.Products;

public class DeleteProductResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = "Product deleted successfully";
}