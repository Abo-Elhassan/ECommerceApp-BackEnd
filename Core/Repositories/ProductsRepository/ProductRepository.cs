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
        public async Task<Product> GetProductByIdCategoryAsync(Guid id)
        {
            return await _storecontext.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id==id);
        }

        public async Task<(IEnumerable<Product>, PaginationMetadata)> GetProductByNameAsync(string name, string searchQuery, int pageNum, int pageSize)
        {
            var collection = _storecontext.Products as IQueryable<Product>;
            if (!string.IsNullOrEmpty(name))
            {
                name=name.Trim();
                collection = collection.Where(p => p.Name == name || p.Category.CategoryName==name);
            }
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery=searchQuery.Trim().ToLower();
                collection = collection.Where(p => p.Name.Contains(searchQuery)|| (p.Description != null && p.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNum);


            var collectionToReturn = await collection.OrderBy(a => a.Name)
                .Include(c => c.Category)
                .Skip(pageSize * (pageNum-1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);

        }

        public async Task<List<Product>> GetProductBySortAsync(string sort, int pageNum, int pageSize)
        {
            if (sort == "name")
            {
                return await _storecontext.Products
                .Include(c => c.Category)
                .OrderBy(p => p.Name)
                .Skip(pageSize * (pageNum-1))
                .Take(pageSize)
                .ToListAsync();
            }
            if (sort == "PriceAsc") { 
            return await _storecontext.Products
                .Include(c => c.Category)
                .OrderBy(p => p.Price)
                .Skip(pageSize * (pageNum-1))
                .Take(pageSize)       
                .ToListAsync();
            }
            if (sort == "PriceDesc") { 
                return await _storecontext.Products
                    .Include(c => c.Category)
                    .OrderByDescending(p => p.Price)
                    .Skip(pageSize * (pageNum-1))
                    .Take(pageSize)
                    .ToListAsync();
            }
            return await _storecontext.Products.ToListAsync();

        }

        public async Task<List<Product>> GetProductByCategoryAsync(string categoryName, int pageNum, int pageSize)
        {

            if (categoryName == "All")
            {
                return await _storecontext.Products.Skip(pageSize * (pageNum-1))
                .Take(pageSize)
                .OrderBy(p => p.Name)
                .Include(c => c.Category)
                .ToListAsync();
            }
            return await _storecontext.Products.Include(p => p.Category)
                .Where(c => c.Category.CategoryName == categoryName)
                .Skip(pageSize * (pageNum-1))
                .Take(pageSize)
                .OrderBy(p => p.Name)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _storecontext.Categories.ToListAsync();
        }
    }

}
