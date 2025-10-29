using BugStore.Models;
using BugStore.Data;
using Microsoft.EntityFrameworkCore;
using BugStore.Requests.Products;
using BugStore.Responses.Products;

namespace BugStore.Handlers.Products;

public static class ProductHandler
{
	public static async Task<IResult> GetAllProducts(AppDbContext db)
	{
		var products = await db.Products.ToListAsync();
		var response = products.Select(p => new GetProductResponse
		{
			Id = p.Id,
			Title = p.Title,
			Description = p.Description,
			Slug = p.Slug,
			Price = p.Price
		});
		return Results.Ok(response);
	}

	public static async Task<IResult> GetProductById(Guid id, AppDbContext db)
	{
		var product = await db.Products.FindAsync(id);
		if (product == null)
			return Results.NotFound(new { Message = "Product not found" });

		var response = new GetProductByIdResponse
		{
			Id = product.Id,
			Title = product.Title,
			Description = product.Description,
			Slug = product.Slug,
			Price = product.Price,
			CreatedAt = DateTime.UtcNow
		};
		return Results.Ok(response);
	}

	public static async Task<IResult> CreateProduct(CreateProductRequest request, AppDbContext db)
	{
		if (string.IsNullOrWhiteSpace(request.Title))
			return Results.BadRequest(new { Message = "Title is required" });

		if (request.Price <= 0)
			return Results.BadRequest(new { Message = "Price must be greater than 0" });

		var product = new Product
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			Description = request.Description,
			Price = request.Price,
			Slug = GenerateSlug(request.Title)
		};

		db.Products.Add(product);
		await db.SaveChangesAsync();

		var response = new CreateProductResponse
		{
			Id = product.Id,
			Title = product.Title,
			Description = product.Description,
			Slug = product.Slug,
			Price = product.Price
		};

		return Results.Created($"/v1/products/{product.Id}", response);
	}

	public static async Task<IResult> UpdateProduct(Guid id, UpdateProductRequest request, AppDbContext db)
	{
		var product = await db.Products.FindAsync(id);
		if (product == null)
			return Results.NotFound(new { Message = "Product not found" });

		if (string.IsNullOrWhiteSpace(request.Title))
			return Results.BadRequest(new { Message = "Title is required" });

		if (request.Price <= 0)
			return Results.BadRequest(new { Message = "Price must be greater than 0" });

		product.Title = request.Title;
		product.Description = request.Description;
		product.Price = request.Price;
		product.Slug = GenerateSlug(request.Title);

		await db.SaveChangesAsync();
		var response = new UpdateProductResponse
		{
			Id = product.Id,
			Title = product.Title,
			Description = product.Description,
			Slug = product.Slug,
			Price = product.Price
		};
		return Results.Ok(response);
	}

	public static async Task<IResult> DeleteProduct(Guid id, AppDbContext db)
	{
		var product = await db.Products.FindAsync(id);
		if (product == null)
			return Results.NotFound(new { Message = "Product not found" });

		db.Products.Remove(product);
		await db.SaveChangesAsync();
		var response = new DeleteProductResponse
		{
			Id = product.Id,
			Title = product.Title
		};
		return Results.Ok(response);
	}

	private static string GenerateSlug(string title)
	{
		return title.ToLower()
			.Replace(" ", "-")
			.Replace("--", "-")
			.Trim('-');
	}
}