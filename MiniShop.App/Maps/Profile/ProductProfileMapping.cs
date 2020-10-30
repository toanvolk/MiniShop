using AutoMapper;
using MiniShop.EF;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Description))
                .AfterMap((source, destination) =>
                {
                    destination.CreatedBy = "ADMIN";
                    destination.CreatedDate = DateTime.Now;

                    destination.UpdatedBy = "ADMIN";
                    destination.UpdatedDate = DateTime.Now;
                });

            //get
            //create - update
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Description));
            //load
            CreateMap<List<Product>, List<ProductDto>>().ConvertUsing<ProductTypingConvert>();
        }
    }
    public class ProductTypingConvert : ITypeConverter<List<Product>, List<ProductDto>>
    {
        public List<ProductDto> Convert(List<Product> source, List<ProductDto> destination, ResolutionContext context)
        {
            destination ??= new List<ProductDto>();
            foreach (var item in source)
            {
                destination.Add(new ProductDto() { 
                    Id = item.Id,
                    Description = item.Description,
                    Name = item.Name,
                    NotUse = item.NotUse
                });
            }
            return destination;
        }
    }
}
