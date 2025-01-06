using api.Core.Interfaces;
using api.Data.Models;
using api.Data;

using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class ProductService : BaseService<Product>, IProductService
{
    public ProductService(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.category)
            .Where(p => !p.is_deleted)
            .ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.category)
            .FirstOrDefaultAsync(p => p.id == id && !p.is_deleted);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => p.category_id == categoryId && !p.is_deleted)
            .Include(p => p.category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => !p.is_deleted &&
                ((p.name != null && p.name.Contains(searchTerm)) ||
                 (p.description != null && p.description.Contains(searchTerm))))
            .Include(p => p.category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => !p.is_deleted && p.price >= minPrice && p.price <= maxPrice)
            .Include(p => p.category)
            .ToListAsync();
    }

    public override async Task<Product> CreateAsync(Product product)
    {
        await ValidateProduct(product);
        return await base.CreateAsync(product);
    }

    public override async Task<Product?> UpdateAsync(int id, Product product)
    {
        var existingProduct = await _dbSet.FindAsync(id);
        if (existingProduct == null || existingProduct.is_deleted) return null;

        await ValidateProduct(product, id);

        existingProduct.name = product.name;
        existingProduct.description = product.description;
        existingProduct.price = product.price;
        existingProduct.stock = product.stock;
        existingProduct.category_id = product.category_id;

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public override async Task<bool> SoftDeleteAsync(int id)
    {
        var product = await _dbSet
            .Include(p => p.product_images)
            .FirstOrDefaultAsync(p => p.id == id);

        if (product == null || product.is_deleted) return false;

        // Delete all associated images
        _context.ProductImages.RemoveRange(product.product_images);

        product.is_deleted = true;
        product.deleted_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<bool> HardDeleteAsync(int id)
    {
        var product = await _dbSet
            .Include(p => p.product_images)
            .FirstOrDefaultAsync(p => p.id == id);

        if (product == null) return false;

        // Images will be automatically deleted due to cascade delete in DB
        _dbSet.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<IEnumerable<Product>> GetDeletedAsync()
    {
        return await _dbSet
            .Where(p => p.is_deleted)
            .Include(p => p.category)
            .ToListAsync();
    }

    public override async Task<bool> RestoreAsync(int id)
    {
        var product = await _dbSet.FindAsync(id);
        if (product == null || !product.is_deleted) return false;

        product.is_deleted = false;
        product.deleted_at = null;
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateProduct(Product product, int? excludeId = null)
    {
        // Validate category exists
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.id == product.category_id && !c.is_deleted);
        if (!categoryExists)
        {
            throw new ArgumentException("Category does not exist or is deleted");
        }

        // Validate price
        if (product.price <= 0)
        {
            throw new ArgumentException("Price must be greater than 0");
        }

        // Validate stock quantity
        if (product.stock < 0)
        {
            throw new ArgumentException("Stock quantity cannot be negative");
        }

        // Check for duplicate names in the same category
        var duplicateQuery = _dbSet.Where(p =>
            p.name.ToLower() == product.name.ToLower() &&
            p.category_id == product.category_id &&
            !p.is_deleted);

        if (excludeId.HasValue)
        {
            duplicateQuery = duplicateQuery.Where(p => p.id != excludeId);
        }

        if (await duplicateQuery.AnyAsync())
        {
            throw new InvalidOperationException("A product with this name already exists in the selected category");
        }
    }
}
