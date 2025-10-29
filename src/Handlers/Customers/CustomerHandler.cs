using BugStore.Models;
using BugStore.Data;
using Microsoft.EntityFrameworkCore;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;

namespace BugStore.Handlers.Customers;

public static class CustomerHandler
{
    public static async Task<IResult> GetAllCustomers(AppDbContext db)
    {
        var customers = await db.Customers.ToListAsync();
        var response = customers.Select(c => new GetCustomerResponse
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone,
            TotalOrders = c.Orders?.Count ?? 0
        });
        return Results.Ok(response);
    }

    public static async Task<IResult> GetCustomerById(Guid id, AppDbContext db)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer == null)
            return Results.NotFound(new { Message = "Customer not found" });

        var response = new GetCustomerByIdResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            CreatedAt = DateTime.UtcNow,
            TotalOrders = customer.Orders?.Count ?? 0,
            TotalSpent = 0,
            RecentOrders = new()
        };

        return Results.Ok(response);
    }

    public static async Task<IResult> CreateCustomer(CreateCustomerRequest request, AppDbContext db)
    {
        // Validações básicas
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest(new { Message = "Name is required" });

        if (string.IsNullOrWhiteSpace(request.Email))
            return Results.BadRequest(new { Message = "Email is required" });

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone
        };

        db.Customers.Add(customer);
        await db.SaveChangesAsync();

        var response = new CreateCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            CreatedAt = DateTime.UtcNow
        };

        return Results.Created($"/v1/customers/{customer.Id}", response);
    }

    public static async Task<IResult> UpdateCustomer(Guid id, UpdateCustomerRequest request, AppDbContext db)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer == null)
            return Results.NotFound(new { Message = "Customer not found" });

        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest(new { Message = "Name is required" });

        if (string.IsNullOrWhiteSpace(request.Email))
            return Results.BadRequest(new { Message = "Email is required" });

        customer.Name = request.Name;
        customer.Email = request.Email;
        customer.Phone = request.Phone;

        await db.SaveChangesAsync();
        var response = new UpdateCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            UpdatedAt = DateTime.UtcNow
        };
        return Results.Ok(response);
    }

    public static async Task<IResult> DeleteCustomer(Guid id, AppDbContext db)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer == null)
            return Results.NotFound(new { Message = "Customer not found" });

        db.Customers.Remove(customer);
        await db.SaveChangesAsync();
        var response = new DeleteCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            DeletedAt = DateTime.UtcNow
        };
        return Results.Ok(response);
    }
}