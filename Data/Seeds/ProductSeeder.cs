using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeds;

public static class ProductSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (await context.Products.AnyAsync())
            return;

        var categories = await context.Categories.ToListAsync();
        if (!categories.Any())
            return;

        var products = new List<Product>();

        foreach (var category in categories)
        {
            switch (category.name)
            {
                case "Women's Clothing":
                    products.AddRange(new[]
                    {
                        new Product { name = "Floral Summer Dress", description = "Lightweight and stylish summer dress.", price = 29.99M, stock = 200, category_id = 1 },
                        new Product { name = "Casual Blouse", description = "Elegant and comfortable blouse.", price = 19.99M, stock = 150, category_id = 1 }
                    });
                    break;

                case "Men's Clothing":
                    products.AddRange(new[]
                    {
                        new Product { name = "Casual T-Shirt", description = "Soft cotton casual t-shirt.", price = 14.99M, stock = 300, category_id = 2 },
                        new Product { name = "Formal Suit", description = "Classic formal suit for business meetings.", price = 99.99M, stock = 50, category_id = 2 }
                    });
                    break;

                case "Kids' Clothing":
                    products.AddRange(new[]
                    {
                        new Product { name = "Kids' Hoodie", description = "Warm and cozy hoodie.", price = 24.99M, stock = 100, category_id = 3 },
                        new Product { name = "Cartoon T-Shirt", description = "Cute cartoon-themed t-shirt.", price = 9.99M, stock = 250, category_id = 3 }
                    });
                    break;

                case "Shoes":
                    products.AddRange(new[]
                    {
                        new Product { name = "Running Shoes", description = "Comfortable and durable running shoes.", price = 49.99M, stock = 120, category_id = 4 },
                        new Product { name = "Leather Boots", description = "Stylish leather boots for all occasions.", price = 89.99M, stock = 80, category_id = 4 }
                    });
                    break;

                case "Bags & Accessories":
                    products.AddRange(new[]
                    {
                        new Product { name = "Leather Handbag", description = "Premium leather handbag.", price = 59.99M, stock = 70, category_id = 5 },
                        new Product { name = "Sunglasses", description = "Trendy UV-protected sunglasses.", price = 19.99M, stock = 200, category_id = 5 }
                    });
                    break;

                case "Beauty & Health":
                    products.AddRange(new[]
                    {
                        new Product { name = "Skincare Kit", description = "Complete skincare essentials.", price = 39.99M, stock = 150, category_id = 6 },
                        new Product { name = "Makeup Palette", description = "Vibrant shades for every occasion.", price = 24.99M, stock = 100, category_id = 6 }
                    });
                    break;

                case "Home & Living":
                    products.AddRange(new[]
                    {
                        new Product { name = "Throw Blanket", description = "Cozy and soft throw blanket.", price = 29.99M, stock = 120, category_id = 7 },
                        new Product { name = "Wall Art", description = "Modern decorative wall art.", price = 19.99M, stock = 80, category_id = 7 }
                    });
                    break;

                case "Electronics":
                    products.AddRange(new[]
                    {
                        new Product { name = "Wireless Earbuds", description = "High-quality wireless earbuds.", price = 49.99M, stock = 200, category_id = 8 },
                        new Product { name = "Smart Watch", description = "Feature-rich smart watch.", price = 99.99M, stock = 90, category_id = 8 }
                    });
                    break;

                case "Sports & Outdoors":
                    products.AddRange(new[]
                    {
                        new Product { name = "Yoga Mat", description = "Eco-friendly non-slip yoga mat.", price = 19.99M, stock = 150, category_id = 9 },
                        new Product { name = "Camping Tent", description = "Waterproof and durable camping tent.", price = 129.99M, stock = 40, category_id = 9 }
                    });
                    break;
            }
        }

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
