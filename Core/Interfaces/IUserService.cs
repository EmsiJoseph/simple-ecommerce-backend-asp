using api.Core.DTOs;
using api.Data.Models;

namespace api.Core.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto> CreateAsync(UserCreateDto userDto);
    Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto userDto);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> HardDeleteAsync(int id);
    Task<IEnumerable<UserResponseDto>> GetByRoleAsync(string role);
    Task<IEnumerable<UserResponseDto>> GetDeletedAsync();
    Task<bool> RestoreAsync(int id);
}
