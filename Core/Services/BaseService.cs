using api.Core.Interfaces;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public abstract class BaseService<T> : IBaseService<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseService(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public abstract Task<T?> UpdateAsync(int id, T entity);
    public abstract Task<bool> SoftDeleteAsync(int id);
    public abstract Task<bool> HardDeleteAsync(int id);
    public abstract Task<IEnumerable<T>> GetDeletedAsync();
    public abstract Task<bool> RestoreAsync(int id);
}
