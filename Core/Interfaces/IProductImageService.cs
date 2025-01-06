using api.Data.Models;

namespace api.Core.Interfaces;

public interface IProductImageService
{
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task<ProductImage?> GetByIdAsync(int id);
    Task<ProductImage> CreateAsync(ProductImage productImage);
    Task<ProductImage?> UpdateAsync(int id, ProductImage productImage);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(int productId);
    Task<ProductImage?> GetPrimaryImageByProductIdAsync(int productId);
    Task<bool> SetPrimaryImageAsync(int imageId, int productId);
}
