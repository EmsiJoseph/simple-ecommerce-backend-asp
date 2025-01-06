namespace api.Core.Interfaces;

public interface IBaseService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(int id, T entity);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> HardDeleteAsync(int id);
    Task<IEnumerable<T>> GetDeletedAsync();
    Task<bool> RestoreAsync(int id);
    Task<bool> DeleteAsync(int id);
}
