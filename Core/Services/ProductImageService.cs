using api.Core.Interfaces;
using api.Data.Models;
using api.Data;

using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class ProductImageService: IProductImageService
{
    private readonly AppDbContext _context;
    private readonly DbSet<ProductImage> _dbSet;

    public ProductImageService(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<ProductImage>();
    }

    public async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        return await _dbSet
            .Include(pi => pi.product)
            .ToListAsync();
    }

    public async Task<ProductImage?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(pi => pi.product)
            .FirstOrDefaultAsync(pi => pi.id == id);
    }

    public async Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(int productId)
    {
        return await _dbSet
            .Where(pi => pi.product_id == productId)
            .ToListAsync();
    }

    public async Task<ProductImage?> GetPrimaryImageByProductIdAsync(int productId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(pi => pi.product_id == productId && pi.is_main);
    }


    public async Task<ProductImage> CreateAsync(ProductImage productImage)
    {
        await ValidateProductImage(productImage);

        var existingImages = await GetImagesByProductIdAsync(productImage.product_id);
        if (!existingImages.Any())
        {
            productImage.is_main = true;
        }
        else if (productImage.is_main)
        {
            await RemovePrimaryFromOtherImages(productImage.product_id);
        }

        _dbSet.Add(productImage);
        await _context.SaveChangesAsync();
        return productImage;
    }

    public async Task<ProductImage?> UpdateAsync(int id, ProductImage productImage)
    {
        var existingImage = await _dbSet.FindAsync(id);
        if (existingImage == null) return null;

        await ValidateProductImage(productImage);

        if (productImage.is_main && !existingImage.is_main)
        {
            await RemovePrimaryFromOtherImages(productImage.product_id);
        }

        existingImage.image_url = productImage.image_url;
        existingImage.is_main = productImage.is_main;

        await _context.SaveChangesAsync();
        return existingImage;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image = await _dbSet.FindAsync(id);
        if (image == null) return false;

        if (image.is_main)
        {
            var otherImages = await _dbSet
                .Where(pi => pi.product_id == image.product_id && pi.id != id)
                .ToListAsync();

            if (!otherImages.Any())
            {
                throw new InvalidOperationException("Cannot delete the only primary image of a product");
            }

            var newPrimary = otherImages.First();
            newPrimary.is_main = true;
        }

        _dbSet.Remove(image);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateProductImage(ProductImage productImage)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.id == productImage.product_id && !p.is_deleted);
        if (!productExists)
        {
            throw new ArgumentException("Product does not exist or is deleted");
        }

        if (!Uri.IsWellFormedUriString(productImage.image_url, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid image URL format");
        }
    }

    private async Task RemovePrimaryFromOtherImages(int productId)
    {
        var primaryImages = await _dbSet
            .Where(pi => pi.product_id == productId && pi.is_main)
            .ToListAsync();

        foreach (var image in primaryImages)
        {
            image.is_main = false;
        }
    }

    public async Task<bool> SetPrimaryImageAsync(int imageId, int productId)
    {
        var image = await _dbSet.FindAsync(imageId);
        if (image == null || image.product_id != productId) return false;

        await RemovePrimaryFromOtherImages(productId);
        image.is_main = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
