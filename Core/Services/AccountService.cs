using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace api.Core.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountResponseDto>> GetAllAsync()
    {
        return await _context.Accounts
            .Where(a => !a.is_deleted)
            .Select(a => new AccountResponseDto
            {
                Id = a.id,
                UserInfo = a.user != null ? new UserResponseDto
                {
                    Id = a.user.id,
                    FirstName = a.user.first_name,
                    LastName = a.user.last_name,
                    Gender = a.user.gender,
                    Phone = a.user.phone,
                    AddressLine1 = a.user.address_line1,
                    AddressLine2 = a.user.address_line2,
                    City = a.user.city,
                    State = a.user.state,
                    ZipCode = a.user.zip_code,
                    Country = a.user.country,
                    DateOfBirth = a.user.date_of_birth,
                    CreatedAt = a.user.created_at,
                    UpdatedAt = a.user.updated_at
                } : null,
                Email = a.email,
                Role = a.role,
                LastLogin = a.last_login,
                AccountStatus = a.account_status,
                CreatedAt = a.created_at,
                UpdatedAt = a.updated_at
            })
            .ToListAsync();
    }

    public async Task<AccountResponseDto?> GetByIdAsync(int id)
    {
        var account = await _context.Accounts
            .Include(a => a.user)
            .FirstOrDefaultAsync(a => a.id == id && !a.is_deleted);
            
        if (account == null) return null;

        return new AccountResponseDto
        {
            Id = account.id,
            UserInfo = account.user != null ? new UserResponseDto
            {
                Id = account.user.id,
                FirstName = account.user.first_name,
                LastName = account.user.last_name,
                Gender = account.user.gender,
                Phone = account.user.phone,
                AddressLine1 = account.user.address_line1,
                AddressLine2 = account.user.address_line2,
                City = account.user.city,
                State = account.user.state,
                ZipCode = account.user.zip_code,
                Country = account.user.country,
                DateOfBirth = account.user.date_of_birth,
                CreatedAt = account.user.created_at,
                UpdatedAt = account.user.updated_at
            } : null,
            Email = account.email,
            Role = account.role,
            LastLogin = account.last_login,
            AccountStatus = account.account_status,
            CreatedAt = account.created_at,
            UpdatedAt = account.updated_at
        };
    }

    public async Task<AccountResponseDto> CreateAsync(AccountCreateDto accountDto)
    {
        // Validate unique email
        if (await _context.Accounts.AnyAsync(a => a.email == accountDto.Email))
            throw new InvalidOperationException("Email already exists");

        // Create user first
        var user = new User
        {
            first_name = accountDto.FirstName,
            last_name = accountDto.LastName,
            gender = accountDto.Gender,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Then create account
        var account = new Account
        {
            email = accountDto.Email,
            password_hash = HashPassword(accountDto.Password),
            user_id = user.id,
            role = accountDto.Role,
            account_status = "Active",
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return new AccountResponseDto
        {
            Id = account.id,
            UserInfo = new UserResponseDto
            {
                Id = user.id,
                FirstName = user.first_name,
                LastName = user.last_name,
                CreatedAt = user.created_at,
                UpdatedAt = user.updated_at
            },
            Email = account.email,
            Role = account.role,
            LastLogin = account.last_login,
            AccountStatus = account.account_status,
            CreatedAt = account.created_at,
            UpdatedAt = account.updated_at
        };
    }

    public async Task<AccountResponseDto?> UpdateAsync(int id, AccountUpdateDto accountDto)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.id == id && !a.is_deleted);
        if (account == null) return null;

        if (accountDto.Email != null)
        {
            if (await _context.Accounts.AnyAsync(a => a.email == accountDto.Email && a.id != id))
                throw new InvalidOperationException("Email already exists");
            account.email = accountDto.Email;
        }

        if (accountDto.Password != null)
            account.password_hash = HashPassword(accountDto.Password);

        if (accountDto.Role != null)
            account.role = accountDto.Role;

        if (accountDto.AccountStatus != null)
            account.account_status = accountDto.AccountStatus;

        account.updated_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return new AccountResponseDto
        {
            Id = account.id,
            Email = account.email,
            Role = account.role,
            LastLogin = account.last_login,
            AccountStatus = account.account_status,
            CreatedAt = account.created_at,
            UpdatedAt = account.updated_at
        };
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.id == id && !a.is_deleted);
        if (account == null) return false;

        account.is_deleted = true;
        account.deleted_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HardDeleteAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) return false;

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
