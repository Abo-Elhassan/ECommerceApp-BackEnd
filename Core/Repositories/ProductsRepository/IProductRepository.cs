using Core.Entities;
using Core.Repositories.GenericRepository;

namespace Core.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdCategoryAsync(Guid id);
        Task<(IEnumerable<Product>,PaginationMetadata)> GetProductByNameAsync(string name, string searchQuery, int pageNum, int pageSize);
        Task<List<Product>> GetProductByPriceAsync(decimal minPrice, decimal maxPrice, int pageNum, int pageSize);
        Task<List<Product>> GetProductByCategoryAsync(string categoryName, int pageNum, int pageSize);



    }
}
