using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                 // .ForMember(user => user.Id, opt => opt.MapFrom(src => src.UserId)
                       .ForAllMembers(m => m.Condition((source, target, sourceValue, targetValue) => sourceValue != null));
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<ProductUpdateDto, Product>()
            .ForMember(product => product.Id, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductImagesDto, ProductImage>();
        }
    }
}
