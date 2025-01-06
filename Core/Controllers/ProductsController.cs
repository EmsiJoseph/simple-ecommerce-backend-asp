using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Core.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("deleted")]

    public async Task<ActionResult<IEnumerable<Product>>> GetDeleted()
    {
        var products = await _productService.GetDeletedAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }


    [HttpGet("search/{searchTerm}")]
    public async Task<ActionResult<IEnumerable<Product>>> Search(string searchTerm)
    {
        var products = await _productService.SearchProductsAsync(searchTerm);
        return Ok(products);
    }

    [HttpGet("price/{minPrice}/{maxPrice}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByPriceRange(decimal minPrice, decimal maxPrice)
    {
        var products = await _productService.GetProductsByPriceRangeAsync(minPrice, maxPrice);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] ProductDto productDto)
    {
        try
        {
            var product = new Product
            {
                name = productDto.name,
                description = productDto.description,
                price = productDto.price,
                stock = productDto.stock,
                category_id = productDto.category_id
            };

            var created = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] ProductDto productDto)
    {
        try
        {
            var product = new Product
            {
                name = productDto.name,
                description = productDto.description,
                price = productDto.price,
                stock = productDto.stock,
                category_id = productDto.category_id
            };

            var updated = await _productService.UpdateAsync(id, product);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> SoftDelete(int id)
    {
        var deleted = await _productService.SoftDeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}/hard")]
    public async Task<ActionResult> HardDelete(int id)
    {
        var deleted = await _productService.HardDeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}/restore")]
    public async Task<ActionResult> Restore(int id)
    {
        var restored = await _productService.RestoreAsync(id);
        if (!restored) return NotFound();
        return NoContent();
    }


}