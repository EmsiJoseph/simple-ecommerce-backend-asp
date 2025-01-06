using api.Core.DTOs;
using api.Data.Models;

namespace api.Core.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountResponseDto>> GetAllAsync();
    Task<AccountResponseDto?> GetByIdAsync(int id);
    Task<AccountResponseDto> CreateAsync(AccountCreateDto accountDto);
    Task<AccountResponseDto?> UpdateAsync(int id, AccountUpdateDto accountDto);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> HardDeleteAsync(int id);
}
