using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class CardService : ICardService
{
    private readonly AppDbContext _context;

    public CardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Card>> GetAllAsync()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }

    public async Task<Card> CreateAsync(Card cardDto)
    {
        var card = new Card
        {
            customer_id = cardDto.customer_id,
            card_brand = cardDto.card_brand,
            card_token = cardDto.card_token,
            expiration_month = cardDto.expiration_month,
            expiration_year = cardDto.expiration_year,
            last4 = cardDto.last4,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<Card?> UpdateAsync(int id, Card cardDto)
    {
        var card = await _context.Cards.FindAsync(id);
        if (card == null) return null;

        card.card_brand = cardDto.card_brand;
        card.card_token = cardDto.card_token;
        card.expiration_month = cardDto.expiration_month;
        card.expiration_year = cardDto.expiration_year;
        card.last4 = cardDto.last4;
        card.updated_at = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var card = await _context.Cards.FindAsync(id);
        if (card == null) return false;

        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
        return true;
    }
}
