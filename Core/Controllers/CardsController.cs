using api.Core.DTOs;
using api.Data.Models;
using api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        try
        {
            var cards = await _cardService.GetAllAsync();
            return Ok(cards);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCard(int id)
    {
        try
        {
            var card = await _cardService.GetByIdAsync(id);
            if (card == null) return NotFound();
            return Ok(card);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] Card cardDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var newCard = await _cardService.CreateAsync(cardDto);
            return CreatedAtAction(nameof(GetCard), new { id = newCard.id }, newCard);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCard(int id, [FromBody] Card cardDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedCard = await _cardService.UpdateAsync(id, cardDto);
            if (updatedCard == null) return NotFound();
            return Ok(updatedCard);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard(int id)
    {
        try
        {
            var result = await _cardService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
