using System;
using System.Collections.Generic;
using AutoMapper;
using DCommerce.Data.Domain;
using DCommerce.Dto.Requests.Category;
using DCommerce.Dto.Requests.Product;
using DCommerce.Dto.Responses;
using DCommerce.Repository.Extensions;

namespace DCommerce.Dto.Shared
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();
            CreateMap<Product, ProductDto>()
                .ForMember(src => src.UnitOfMeasurement,
                           opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<ApplicationUser, UserProfileDto>();
        }
    }
}
