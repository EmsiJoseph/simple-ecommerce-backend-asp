using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeds;

public static class DatabaseSeeder
{
    public static async Task SeedDatabase(AppDbContext context)
    {
        if (context.Database.GetPendingMigrations().Any())
        {
            await context.Database.MigrateAsync();
        }

        await CategorySeeder.Seed(context);
        await ProductSeeder.Seed(context);
        await AccountSeeder.Seed(context);
    }
}
