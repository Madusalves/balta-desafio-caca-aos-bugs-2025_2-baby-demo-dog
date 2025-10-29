namespace BugStore.Requests.Products;

public class GetProductByIdRequest
{
    public bool IncludeOrders { get; set; } = false;
}