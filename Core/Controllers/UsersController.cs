using Microsoft.AspNetCore.Mvc;
using api.Core.DTOs;
using api.Core.Interfaces;

namespace api.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // [HttpPost]
    // public async Task<ActionResult<UserResponseDto>> Create(UserWithAccountCreateDto dto)
    // {
    //     try
    //     {
    //         var user = await _userService.CreateWithAccountAsync(dto);
    //         return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    //     }
    //     catch (InvalidOperationException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    // }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDto>> Update(int id, UserUpdateDto userDto)
    {
        try
        {
            var user = await _userService.UpdateAsync(id, userDto);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var result = await _userService.SoftDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDelete(int id)
    {
        var result = await _userService.HardDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await _userService.RestoreAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("role/{role}")]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetByRole([FromRoute] string role)
    {
        try
        {
            var users = await _userService.GetByRoleAsync(role);
            return Ok(users);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("restore/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> RestoreAsync(int id)
    {
        var result = await _userService.RestoreAsync(id);
        if (!result)
            return NotFound();

        return Ok(result);
    }
}
