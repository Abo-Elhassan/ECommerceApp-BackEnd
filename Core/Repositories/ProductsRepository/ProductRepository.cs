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

        public async Task<Product> GetProductCategoriesAsync(string CatName)
        {
            return await _storecontext.Products.Include(p => p.Category).FirstOrDefaultAsync(P => P.Category.CategoryName == CatName);
        }
    }
}
