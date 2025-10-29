using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Orders;

public static class OrderHandler
{
    public static async Task<IResult> GetOrdersById(Guid id, AppDbContext db)
    {
        var order = await db.Orders
            .Include(o => o.Lines)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return Results.NotFound(new { Message = "Order not found" });

        var response = new BugStore.Responses.Orders.GetOrdersByIdResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Total = order.Total
        };

        return Results.Ok(response);
    }

    public static async Task<IResult> CreateOrders(CreateOrdersRequest request, AppDbContext db)
    {
        if (request == null)
            return Results.BadRequest(new { Message = "Invalid request" });

        if (request.CustomerId == Guid.Empty)
            return Results.BadRequest(new { Message = "CustomerId is required" });

        if (request.Items == null || request.Items.Count == 0)
            return Results.BadRequest(new { Message = "At least one item is required" });

        var customerExists = await db.Customers.AnyAsync(c => c.Id == request.CustomerId);
        if (!customerExists)
            return Results.BadRequest(new { Message = "Customer not found" });

        var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = await db.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        if (products.Count != productIds.Count)
            return Results.BadRequest(new { Message = "One or more products not found" });

        var order = new Order
        {
            CustomerId = request.CustomerId
        };

        foreach (var item in request.Items)
        {
            var product = products[item.ProductId];
            order.AddItem(product, item.Quantity);
        }

        db.Orders.Add(order);
        await db.SaveChangesAsync();

        var response = new BugStore.Responses.Orders.CreateOrdersResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CreatedAt = order.CreatedAt,
            Total = order.Total
        };

        return Results.Created($"/v1/orders/{order.Id}", response);
    }
}