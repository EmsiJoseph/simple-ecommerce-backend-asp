using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("deleted")]
    public async Task<ActionResult<IEnumerable<Category>>> GetDeleted()
    {
        var categories = await _categoryService.GetDeletedAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpGet("parent/{parentId}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetByParentId(int parentId)
    {
        var categories = await _categoryService.GetCategoriesByParentIdAsync(parentId);
        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Create([FromBody] CategoryDto categoryDto)
    {
        try
        {
            var category = new Category
            {
                name = categoryDto.name,
                description = categoryDto.description,
                parent_id = categoryDto.parent_id
            };

            var created = await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> Update(int id, [FromBody] CategoryDto categoryDto)
    {
        try
        {
            var category = new Category
            {
                name = categoryDto.name,
                description = categoryDto.description,
                parent_id = categoryDto.parent_id
            };

            var updated = await _categoryService.UpdateAsync(id, category);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)

    {
        var result = await _categoryService.SoftDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDelete(int id)
    {
        var result = await _categoryService.HardDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await _categoryService.RestoreAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
