using Core.Entities;
using Core.Repositories.GenericRepository;

namespace Core.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductCategoriesAsync(string CatName);
    }
}
