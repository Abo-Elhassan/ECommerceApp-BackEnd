using Core.Context;
using Core.Entities;
using Core.Repositories.GenericRepository;
using Core;
using Microsoft.EntityFrameworkCore;
using API;

namespace Core.Repositories.ProductsRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StoreContext _storecontext;

        public ProductRepository(StoreContext storecontext) : base(storecontext)
        {
            _storecontext = storecontext;
        }
        #region Get Product With Id
        public async Task<Product> GetProductByIdCategoryAsync(Guid id)
        {
            return await _storecontext.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);
        }
        #endregion

        #region Get All Categories
        public async Task<List<Category>> GetCategories()
        {
            return await _storecontext.Categories.ToListAsync();
        }
        #endregion

        #region Multi-Purpose End-Point
        public async Task<ProductParams> GetMulti(ProductParams prdParams)
        {
            var DBcollection = _storecontext.Products as IQueryable<Product>;
            IQueryable<Product> returnCol = null;

            if (prdParams.CategoryName == "All")
            {
                returnCol = DBcollection
                            .OrderBy(p => p.Name)
                            .Include(c => c.Category);
                prdParams.TotalItemCount = await DBcollection.CountAsync();
            }
            if (prdParams.CategoryName != "All" && prdParams.CategoryName is not null)
            {
                returnCol = DBcollection.OrderBy(p => p.Name)
                .Include(c => c.Category)
                .Where(cp => cp.Category.CategoryName == prdParams.CategoryName);
                prdParams.TotalItemCount = await returnCol.CountAsync();
            }
            if (prdParams.Sort == "PriceAsc")
            {
                returnCol = returnCol.OrderBy(c => c.Price);

            }
            if (prdParams.Sort == "PriceDesc")
            {
                returnCol = returnCol.OrderByDescending(c => c.Price);
            }
            if (!string.IsNullOrWhiteSpace(prdParams.SearchQuery))
            {
                prdParams.SearchQuery = prdParams.SearchQuery.Trim().ToLower();
                returnCol = returnCol.Where(p => p.Name.Contains(prdParams.SearchQuery) || (p.Description != null && p.Description.Contains(prdParams.SearchQuery)));
                prdParams.TotalItemCount = await returnCol.CountAsync();
            }
            prdParams.ProductsReturn = await returnCol
                .Skip(prdParams.PageSize * (prdParams.PageNum - 1))
                .Take(prdParams.PageSize).ToListAsync();
            return prdParams;

        } 
        #endregion
    }

}
