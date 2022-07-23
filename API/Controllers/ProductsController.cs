using AutoMapper;
using Core.Repositories.ProductsRepository;
using Infrastructure.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        #region Get all Products without Categories

        [HttpGet("allProducts")]

        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAllProducts(int pageNum, int takes)
        {
            var listFromDb = await _productsRepository.GetAllAsync(pageNum, takes);
            return _mapper.Map<List<ProductReadDTO>>(listFromDb);
        }

        #endregion

        #region Get all Products with Categories
        // GET: api/Products
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProductsWithCategory(int pageNum, int takes)
        {
            var listFromDb = await _productsRepository.GetProductCategoriesAsync(pageNum, takes);
            return _mapper.Map<List<ProductReadDTO>>(listFromDb);
        }
        #endregion

        #region Get Product Without Category
            // GET: api/Products/5
            [HttpGet("{id}")]
            public async Task<ActionResult<ProductReadDTO>> GetProduct(Guid id)
            {
                var product = await _productsRepository.GetByIdAsync(id);
                if (product is null)
                {
                    return NotFound();
                }

                return _mapper.Map<ProductReadDTO>(product);
            }
        #endregion

        #region Get Product Without Category
        [HttpGet("category/{id:int}")]
        public async Task<ActionResult<ProductReadDTO>> GetProductById(Guid id)
        {
            var product = await _productsRepository.GetProductByIdCategoryAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductReadDTO>(product);
        }


        #endregion

        #region Search By Product Name
        [HttpGet("product/{name}")]
        public async Task<ActionResult<List<ProductReadDTO>>> GetProductByName(string name)
        {
            var product = await _productsRepository.GetProductByNameAsync(name);

            if (product is null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductReadDTO>>(product);
        }
        #endregion
        #region Search By Product Price
        [HttpGet("category/price")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProductByPrice(decimal minPrice, decimal maxPrice)
        {
            var product = await _productsRepository.GetProductByPriceAsync(minPrice, maxPrice);

            if (product is null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductReadDTO>>(product);
        }
        #endregion
        #region Search By Category Name
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProductByCategory(string categoryName)
        {
            var product = await _productsRepository.GetProductByCategoryAsync(categoryName);

            if (product is null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductReadDTO>>(product);
        }
        #endregion
    }
}
