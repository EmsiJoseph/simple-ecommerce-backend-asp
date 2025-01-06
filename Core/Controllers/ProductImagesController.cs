using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImagesController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetAll()
    {
        var images = await _productImageService.GetAllAsync();
        return Ok(images);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductImage>> GetById(int id)
    {
        var image = await _productImageService.GetByIdAsync(id);
        if (image == null) return NotFound();
        return Ok(image);
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetByProductId(int productId)
    {
        var images = await _productImageService.GetImagesByProductIdAsync(productId);
        return Ok(images);
    }

    [HttpGet("product/{productId}/primary")]
    public async Task<ActionResult<ProductImage>> GetPrimaryImage(int productId)
    {
        var image = await _productImageService.GetPrimaryImageByProductIdAsync(productId);
        if (image == null) return NotFound();
        return Ok(image);
    }

    [HttpPost]
    public async Task<ActionResult<ProductImage>> Create([FromBody] ProductImageDto imageDto)
    {
        try
        {
            var productImage = new ProductImage
            {
                product_id = imageDto.product_id,
                image_url = imageDto.image_url,
                is_main = imageDto.is_main
            };

            var created = await _productImageService.CreateAsync(productImage);
            return CreatedAtAction(nameof(GetById), new { id = created.id }, created);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductImage>> Update(int id, [FromBody] ProductImageDto imageDto)
    {
        try
        {
            var productImage = new ProductImage
            {
                product_id = imageDto.product_id,
                image_url = imageDto.image_url,
                is_main = imageDto.is_main
            };

            var updated = await _productImageService.UpdateAsync(id, productImage);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("{imageId}/set-primary/{productId}")]
    public async Task<IActionResult> SetPrimary(int imageId, int productId)
    {
        try
        {
            var result = await _productImageService.SetPrimaryImageAsync(imageId, productId);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _productImageService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
