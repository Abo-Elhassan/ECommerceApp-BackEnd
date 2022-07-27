using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Infrastructure.DTOs.Account;
using Infrastructure.DTOs.Basket;
using Infrastructure.DTOs.Category;
using Infrastructure.DTOs.Customer;
using Infrastructure.DTOs.Order;
using Infrastructure.DTOs.Product;

namespace Infrastructure.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerReadDTO>();
            CreateMap<Customer, UserDto>();
            CreateMap<Product, ProductReadDTO>();
            CreateMap<Category, CategoryReadDTO>();
            CreateMap<RegisterDto, Customer>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>();
         

            //there's no an address class 
            CreateMap<AddressDto, Address>();



        }
    }
}
