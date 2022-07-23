using AutoMapper;
using Core.Entities;
using Infrastructure.DTOs.Customer;
using Infrastructure.DTOs.Product;

namespace Infrastructure.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerReadDTO>();
            CreateMap<Product, ProductReadDTO>();

        }
    }
}
