using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeds;

public static class CategorySeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new Category { name = "Women's Clothing" },
            new Category { name = "Men's Clothing" },
            new Category { name = "Kids' Clothing" },
            new Category { name = "Shoes" },
            new Category { name = "Bags & Accessories" },
            new Category { name = "Beauty & Health" },
            new Category { name = "Home & Living" },
            new Category { name = "Electronics" },
            new Category { name = "Sports & Outdoors" }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
