using api.Data.Models;

namespace api.Core.Interfaces;

public interface ICategoryService : IBaseService<Category>
{
    Task<IEnumerable<Category>> GetCategoriesByParentIdAsync(int parentId);
}
