using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Seeds;

public static class AccountSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (await context.Accounts.AnyAsync())
            return;

        // First create a customer for the admin account
        var adminCustomer = new User
        {
            first_name = "Admin",
            last_name = "User",
            gender = "M",
        };

        await context.Users.AddAsync(adminCustomer);
        await context.SaveChangesAsync();

        var adminAccount = new Account
        {
            user_id = adminCustomer.id,
            email = "admin@example.com",
            password_hash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            role = "Admin",
            account_status = "Active"
        };

        await context.Accounts.AddAsync(adminAccount);
        await context.SaveChangesAsync();
    }
}
