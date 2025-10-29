namespace BugStore.Requests.Products;

public class GetProductsRequest
{
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}