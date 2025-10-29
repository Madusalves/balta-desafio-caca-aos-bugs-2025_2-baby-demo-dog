using BugStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BugStore.Handlers.Customers;
using BugStore.Handlers.Products;
using BugStore.Handlers.Orders;

var builder = WebApplication.CreateBuilder(args);

var cnnString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(cnnString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/v1/customers", CustomerHandler.GetAllCustomers);
app.MapGet("/v1/customers/{id:guid}", CustomerHandler.GetCustomerById);
app.MapPost("/v1/customers", CustomerHandler.CreateCustomer);
app.MapPut("/v1/customers/{id:guid}", CustomerHandler.UpdateCustomer);
app.MapDelete("/v1/customers/{id:guid}", CustomerHandler.DeleteCustomer);

app.MapGet("/v1/products", ProductHandler.GetAllProducts);
app.MapGet("/v1/products/{id:guid}", ProductHandler.GetProductById);
app.MapPost("/v1/products", ProductHandler.CreateProduct);
app.MapPut("/v1/products/{id:guid}", ProductHandler.UpdateProduct);
app.MapDelete("/v1/products/{id:guid}", ProductHandler.DeleteProduct);

app.MapGet("/v1/orders/{id:guid}", OrderHandler.GetOrdersById);
app.MapPost("/v1/orders", OrderHandler.CreateOrders);

app.Run();
