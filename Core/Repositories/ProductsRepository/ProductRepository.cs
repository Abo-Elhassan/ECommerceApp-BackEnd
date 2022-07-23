using Core.Context;
using Core.Entities;
using Core.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.ProductsRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StoreContext _storecontext;

        public ProductRepository(StoreContext storecontext) : base(storecontext)
        {
            _storecontext = storecontext;
        }
        public async Task<List<Product>> GetProductCategoriesAsync(int pageNum,int takeParam)
        {
            int skip = takeParam * (pageNum-1);
            return await _storecontext.Products.Include(p => p.Category).Skip(skip).Take(takeParam).ToListAsync();
        }
        public async Task<Product> GetProductByIdCategoryAsync(Guid id)
        {
            return await _storecontext.Products.Include(p => p.Category).FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<List<Product>> GetProductByNameAsync(string name)
        {
            return await _storecontext.Products.Include(p => p.Category).Where(c => c.Name == name).ToListAsync();
        }

        public async Task<List<Product>> GetProductByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _storecontext.Products.Include(p => p.Category).Where(c => c.Price >= minPrice && c.Price <= maxPrice).ToListAsync();
        }

        public async Task<List<Product>> GetProductByCategoryAsync(string categoryName)
        {
            return await _storecontext.Products.Include(p => p.Category).Where(c => c.Category.CategoryName == categoryName).ToListAsync();
        }
    }
    
}
