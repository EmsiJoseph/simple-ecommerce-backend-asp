using api.Core.Interfaces;
using api.Data.Models;
using api.Data;

using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class CategoryService : BaseService<Category>, ICategoryService
{
    public CategoryService(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Category>> GetCategoriesByParentIdAsync(int parentId)
    {
        return await _dbSet.Where(c => c.parent_id == parentId && !c.is_deleted).ToListAsync();
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbSet.Where(c => !c.is_deleted).ToListAsync();
    }

    public override async Task<Category?> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.id == id && !c.is_deleted);
    }

    public override async Task<Category> CreateAsync(Category category)
    {
        await ValidateCategory(category);
        return await base.CreateAsync(category);
    }

    public override async Task<Category?> UpdateAsync(int id, Category category)
    {
        var existingCategory = await _dbSet.FindAsync(id);
        if (existingCategory == null || existingCategory.is_deleted) return null;

        await ValidateCategory(category, id);

        existingCategory.name = category.name;
        existingCategory.description = category.description;
        existingCategory.parent_id = category.parent_id;
        await _context.SaveChangesAsync();

        return existingCategory;
    }

    public override async Task<bool> SoftDeleteAsync(int id)
    {
        var category = await _dbSet.FindAsync(id);
        if (category == null || category.is_deleted) return false;

        category.is_deleted = true;
        category.deleted_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<bool> HardDeleteAsync(int id)
    {
        var category = await _dbSet.FindAsync(id);
        if (category == null) return false;

        _dbSet.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<IEnumerable<Category>> GetDeletedAsync()
    {
        return await _dbSet.Where(c => c.is_deleted).ToListAsync();
    }

    public override async Task<bool> RestoreAsync(int id)
    {
        var category = await _dbSet.FindAsync(id);
        if (category == null || !category.is_deleted) return false;

        category.is_deleted = false;
        category.deleted_at = null;
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateCategory(Category category, int? excludeId = null)
    {
        // Validate parent category
        if (category.parent_id.HasValue)
        {
            var parentExists = await _dbSet.AnyAsync(c => c.id == category.parent_id && !c.is_deleted);
            if (!parentExists)
            {
                throw new ArgumentException("Parent category does not exist or is deleted");
            }
        }

        // Check for duplicate names
        var duplicateQuery = _dbSet.Where(c => c.name.ToLower() == category.name.ToLower() && !c.is_deleted);
        if (excludeId.HasValue)
        {
            duplicateQuery = duplicateQuery.Where(c => c.id != excludeId);
        }

        if (await duplicateQuery.AnyAsync())
        {
            throw new InvalidOperationException("A category with this name already exists");
        }
    }
}
