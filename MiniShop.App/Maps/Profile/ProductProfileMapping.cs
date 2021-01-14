using AutoMapper;
using Microsoft.Extensions.Options;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class ProductProfileMapping : Profile
    {
        public ProductProfileMapping()
        {
            //create - update
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(source => source.CategoryDto))
                .AfterMap((source, destination) =>
                {
                    destination.Code = source.Name.URLFriendly();

                    destination.CreatedBy = "ADMIN";
                    destination.CreatedDate = DateTime.UtcNow;

                    destination.UpdatedBy = "ADMIN";
                    destination.UpdatedDate = DateTime.UtcNow;
                });

            //get
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryDto, opt => opt.MapFrom(source => source.Category));

            //load
            CreateMap<List<Product>, List<ProductDto>>().ConvertUsing<ProductTypingConvert>();


        }
        public class ProductTypingConvert : ITypeConverter<List<Product>, List<ProductDto>>
        {
            public List<ProductDto> Convert(List<Product> source, List<ProductDto> destination, ResolutionContext context)
            {
                destination ??= new List<ProductDto>();
                foreach (var item in source)
                {
                    destination.Add(context.Mapper.Map<ProductDto>(item));
                }
                return destination;
            }
        }
    }
}
