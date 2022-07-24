using Core.Entities;
using Core.Repositories.GenericRepository;

namespace Core.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdCategoryAsync(Guid id);
        Task<List<Product>> GetProductByNameAsync(string name);
        Task<List<Product>> GetProductByPriceAsync(decimal minPrice,decimal maxPrice);
        Task<List<Product>> GetProductByCategoryAsync(string categoryName);



    }
}
