using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Identity;

namespace eCommerce.Application.Mapping
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, GetProduct>().ReverseMap();
            CreateMap<Product, CreateProduct>().ReverseMap();
            CreateMap<Product, UpdateProduct>().ReverseMap();
            CreateMap<Category, GetCategory>().ReverseMap();
            CreateMap<Category, CreateCategory>().ReverseMap();
            CreateMap<Category, UpdateCategory>().ReverseMap();
            CreateMap<CreateUser, AppUser>();
            CreateMap<LoginUser, AppUser>();
        }
    }
}
