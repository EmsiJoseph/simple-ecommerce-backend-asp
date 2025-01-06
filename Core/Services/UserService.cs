using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IAccountService _accountService;

    public UserService(AppDbContext context, IAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => !u.is_deleted)
            .Select(u => new UserResponseDto
            {
                Id = u.id,
                FirstName = u.first_name,
                LastName = u.last_name,
                Gender = u.gender,
                Phone = u.phone,
                AddressLine1 = u.address_line1,
                AddressLine2 = u.address_line2,
                City = u.city,
                State = u.state,
                ZipCode = u.zip_code,
                Country = u.country,
                DateOfBirth = u.date_of_birth,
                CreatedAt = u.created_at,
                UpdatedAt = u.updated_at
            })
            .ToListAsync();
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.id == id && !u.is_deleted);

        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.id,
            FirstName = user.first_name,
            LastName = user.last_name,
            Gender = user.gender,
            Phone = user.phone,
            AddressLine1 = user.address_line1,
            AddressLine2 = user.address_line2,
            City = user.city,
            State = user.state,
            ZipCode = user.zip_code,
            Country = user.country,
            DateOfBirth = user.date_of_birth,
            CreatedAt = user.created_at,
            UpdatedAt = user.updated_at
        };
    }

    public async Task<UserResponseDto> CreateAsync(UserCreateDto userDto)
    {
        // Validate unique phone
        if (await _context.Users.AnyAsync(u => u.phone == userDto.Phone))
            throw new InvalidOperationException("Phone number already exists");

        var user = new User
        {
            first_name = userDto.FirstName,
            last_name = userDto.LastName,
            gender = userDto.Gender,
            phone = userDto.Phone,
            address_line1 = userDto.AddressLine1,
            address_line2 = userDto.AddressLine2,
            city = userDto.City,
            state = userDto.State,
            zip_code = userDto.ZipCode,
            country = userDto.Country,
            date_of_birth = userDto.DateOfBirth,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.id,
            FirstName = user.first_name,
            LastName = user.last_name,
            Gender = user.gender,
            Phone = user.phone,
            AddressLine1 = user.address_line1,
            AddressLine2 = user.address_line2,
            City = user.city,
            State = user.state,
            ZipCode = user.zip_code,
            Country = user.country,
            DateOfBirth = user.date_of_birth,
            CreatedAt = user.created_at,
            UpdatedAt = user.updated_at
        };
    }

    public async Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto userDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.id == id && !u.is_deleted);

        if (user == null) return null;

        if (userDto.Phone != null && userDto.Phone != user.phone)
        {
            if (await _context.Users.AnyAsync(u => u.phone == userDto.Phone && u.id != id))
                throw new InvalidOperationException("Phone number already exists");
            user.phone = userDto.Phone;
        }

        // Update user properties
        user.first_name = userDto.FirstName ?? user.first_name;
        user.last_name = userDto.LastName ?? user.last_name;
        user.gender = userDto.Gender ?? user.gender;
        user.address_line1 = userDto.AddressLine1 ?? user.address_line1;
        user.address_line2 = userDto.AddressLine2 ?? user.address_line2;
        user.city = userDto.City ?? user.city;
        user.state = userDto.State ?? user.state;
        user.zip_code = userDto.ZipCode ?? user.zip_code;
        user.country = userDto.Country ?? user.country;
        user.date_of_birth = userDto.DateOfBirth ?? user.date_of_birth;
        user.updated_at = DateTime.UtcNow;

        _context.Users.Update(user);  // Explicitly mark the entity as modified
        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.id == id && !u.is_deleted);

        if (user == null) return false;

        user.is_deleted = true;
        user.deleted_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HardDeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserResponseDto>> GetByRoleAsync(string role)
    {
        return await _context.Users
            .Include(u => u.account)
            .Where(u => !u.is_deleted && u.account != null && u.account.role == role)
            .Select(u => new UserResponseDto
            {
                Id = u.id,
                FirstName = u.first_name,
                LastName = u.last_name,
                Gender = u.gender,
                Phone = u.phone,
                AddressLine1 = u.address_line1,
                AddressLine2 = u.address_line2,
                City = u.city,
                State = u.state,
                ZipCode = u.zip_code,
                Country = u.country,
                DateOfBirth = u.date_of_birth,
                CreatedAt = u.created_at,
                UpdatedAt = u.updated_at
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<UserResponseDto>> GetDeletedAsync()
    {
        return await _context.Users
            .Where(u => u.is_deleted)
            .Select(u => new UserResponseDto
            {
                Id = u.id,
                FirstName = u.first_name,
                LastName = u.last_name
            })
            .ToListAsync();
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.id == id && u.is_deleted);

        if (user == null) return false;

        user.is_deleted = false;
        user.deleted_at = null;
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<UserResponseDto> CreateWithAccountAsync(UserWithAccountCreateDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // First create the user
            var user = new User
            {
                first_name = dto.FirstName,
                last_name = dto.LastName,
                gender = "Unspecified",      // Set default value for required gender field
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Then create the account using the new user's ID
            var accountDto = new AccountCreateDto
            {
                Email = dto.Email,
                Password = dto.Password,
                UserId = user.id,
                Role = dto.Role
            };

            await _accountService.CreateAsync(accountDto);
            await transaction.CommitAsync();

            return new UserResponseDto
            {
                Id = user.id,
                FirstName = user.first_name,
                LastName = user.last_name,
                Gender = user.gender,        // Include gender in response
                CreatedAt = user.created_at,
                UpdatedAt = user.updated_at
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException($"Failed to create user with account: {ex.Message}", ex);
        }
    }
}
