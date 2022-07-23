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

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProducts()
        {
            var listFromDb = await _productsRepository.GetAllAsync();
            return _mapper.Map<List<ProductReadDTO>>(listFromDb);
        }

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


    }
}
