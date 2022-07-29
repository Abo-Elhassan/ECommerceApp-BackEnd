using Core.Entities;
using Core.Repositories.GenericRepository;
using API;

namespace Core.Repositories.ProductsRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdCategoryAsync(Guid id);
        Task<List<Category>> GetCategories();
        Task<ProductParams> GetMulti(ProductParams prdParams);


    }
}
