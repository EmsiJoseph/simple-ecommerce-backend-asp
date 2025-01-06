using api.Core.DTOs;
using api.Data.Models;

namespace api.Core.Interfaces;

public interface ICardService
{
    Task<IEnumerable<Card>> GetAllAsync();
    Task<Card?> GetByIdAsync(int id);
    Task<Card> CreateAsync(Card cardDto);
    Task<Card?> UpdateAsync(int id, Card cardDto);
    Task<bool> DeleteAsync(int id);
}
