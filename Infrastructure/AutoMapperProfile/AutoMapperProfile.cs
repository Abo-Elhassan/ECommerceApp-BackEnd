using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Infrastructure.DTOs.Account;
using Infrastructure.DTOs.Basket;
using Infrastructure.DTOs.Category;

using Infrastructure.DTOs.Order;
using Infrastructure.DTOs.Product;

namespace Infrastructure.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
            CreateMap<Customer, UserReadDto>();

            CreateMap<Product, ProductReadDTO>()
                .ForMember(u => u.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Category, CategoryReadDTO>();

            CreateMap<RegisterDto, Customer>();
            CreateMap<Order, OrderReadDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<CustomerBasketDTO, CustomerBasket>();

            //there's no an address class 
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto > ();



        }
    }
}
