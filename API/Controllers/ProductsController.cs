using AutoMapper;
using Core.Repositories.ProductsRepository;
using Infrastructure.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
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
            var listFromDb = await _productsRepository.GetAllAsync(pageNum, takes, x => x.Category);
            return _mapper.Map<List<ProductReadDTO>>(listFromDb);
        }

        #endregion

        #region Get all Products with Categories
        // GET: api/Products
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProductsWithCategory(int pageNum, int takes)
        {
            var listFromDb = await _productsRepository.GetAllAsync(pageNum, takes, m => m.Category);
            //var listFromDb2 = await _productsRepository.GetAllAsync(pageNum, takes, "Category");// the second overload for GetAllAsync function
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
        [HttpGet("product/name")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<ProductReadDTO>>> GetProductByName(string name, string searchQuery, int pageNum = 1, int pageSize = 10)
        {
            if (pageSize>20)
            {
                pageSize = 20;
            }
            var (product,paginationMetadata) = await _productsRepository.GetProductByNameAsync(name, searchQuery, pageNum, pageSize);
           
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<List<ProductReadDTO>>(product));
        }
        #endregion
        #region Search By Product Price
        [HttpGet("category/price")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProductByPrice(decimal minPrice, decimal maxPrice, int pageNum = 1, int pageSize = 10)
        {
            var product = await _productsRepository.GetProductByPriceAsync(minPrice, maxPrice, pageNum, pageSize);

            return _mapper.Map<List<ProductReadDTO>>(product);
        }
        #endregion
        #region Search By Category Name
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProductByCategory(string categoryName, int pageNum = 1, int pageSize = 10)
        {
            var product = await _productsRepository.GetProductByCategoryAsync(categoryName, pageNum, pageSize);

            if (product is null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductReadDTO>>(product);
        }
        #endregion
    }
}
