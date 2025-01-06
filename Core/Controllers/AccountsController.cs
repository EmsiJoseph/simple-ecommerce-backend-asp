using Microsoft.AspNetCore.Mvc;
using api.Core.DTOs;
using api.Core.Interfaces;

namespace api.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponseDto>>> GetAll()
    {
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponseDto>> GetById(int id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<AccountResponseDto>> Create(AccountCreateDto accountDto)
    {
        try
        {
            var account = await _accountService.CreateAsync(accountDto);
            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AccountResponseDto>> Update(int id, AccountUpdateDto accountDto)
    {
        try
        {
            var account = await _accountService.UpdateAsync(id, accountDto);
            if (account == null) return NotFound();
            return Ok(account);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var result = await _accountService.SoftDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDelete(int id)
    {
        var result = await _accountService.HardDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
