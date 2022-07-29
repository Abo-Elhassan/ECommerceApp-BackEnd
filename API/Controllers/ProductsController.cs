using AutoMapper;
using Core.Entities;
using Core.Repositories.ProductsRepository;
using Core;
using Infrastructure.DTOs.Category;
using Infrastructure.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Infrastructure.DTOs;

namespace API.Controllers
{
    [Route("api/products")]
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


        #region Get Product With Id
        [HttpGet("category/{id}")]
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

        #region Get All Categories
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryReadDTO>>> GetAllCategories()
        {
            var categories = await _productsRepository.GetCategories();
            return _mapper.Map<List<CategoryReadDTO>>(categories);
        }
        #endregion

        #region Multi-Purpose End-Point
        [HttpGet("multi")]
        public async Task<ActionResult<ProductParamDTO>> GetMulti(string catName = "All", string sort = "name", string searchQuery = "", int size = 9, int pageNum = 1)
        {
            ProductParams prdparam = new ProductParams
            {
                PageNum = pageNum,
                PageSize = size,
                Sort = sort,
                CategoryName = catName,
                SearchQuery = searchQuery,
            };
            var prd = await _productsRepository.GetMulti(prdparam);
            return _mapper.Map<ProductParamDTO>(prd);
        } 
        #endregion
    }
}
