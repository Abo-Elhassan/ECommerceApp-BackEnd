using Core.Entities;
using Core.Repositories.GenericRepository;

namespace Core.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdCategoryAsync(Guid id);
        Task<(IEnumerable<Product>,PaginationMetadata)> GetProductByNameAsync(string name, string searchQuery, int pageNum, int pageSize);
        Task<(IEnumerable<Product>, PaginationMetadata)> GetProductBySortAsync(string sort, int pageNum, int pageSize);
        Task<(IEnumerable<Product>, PaginationMetadata)> GetProductByCategoryAsync(string categoryName, int pageNum, int pageSize);
        Task<List<Category>> GetCategories();


    }
}
