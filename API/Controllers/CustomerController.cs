using AutoMapper;
using Core.Repositories.CustomerRepository;
using Infrastructure.DTOs.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{

    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository _customererpository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customererpository, IMapper mapper)
        {
            _customererpository = customererpository;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<CustomerReadDTO>>> GetResultAsync(int pageNum, int takeParam)
        {
            var listOfCustomers = await _customererpository.GetAllAsync(pageNum,takeParam);
            return _mapper.Map<List<CustomerReadDTO>>(listOfCustomers);
        }
    }
}
