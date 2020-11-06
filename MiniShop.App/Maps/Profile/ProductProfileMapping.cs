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
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(source => source.CategoryId))
                .ForMember(dest => dest.AreaCode, opt => opt.MapFrom(source => source.AreaCode))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price))
                .ForMember(dest => dest.TrackingLink, opt => opt.MapFrom(source => source.TrackingLink))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(source => source.Picture))
                .ForMember(dest => dest.SmallPicture, opt => opt.MapFrom(source => source.SmallPicture))
                .ForMember(dest => dest.BigPicture, opt => opt.MapFrom(source => source.BigPicture))
                .AfterMap((source, destination) =>
                {
                    destination.CreatedBy = "ADMIN";
                    destination.CreatedDate = DateTime.Now;

                    destination.UpdatedBy = "ADMIN";
                    destination.UpdatedDate = DateTime.Now;
                });

            //get
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Description))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(source => source.CategoryId))
                .ForMember(dest => dest.AreaCode, opt => opt.MapFrom(source => source.AreaCode))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price))
                .ForMember(dest => dest.TrackingLink, opt => opt.MapFrom(source => source.TrackingLink))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(source => source.Picture))
                .ForMember(dest => dest.SmallPicture, opt => opt.MapFrom(source => source.SmallPicture))
                .ForMember(dest => dest.BigPicture, opt => opt.MapFrom(source => source.BigPicture));

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
                    Picture = item.Picture,
                    TrackingLink = item.TrackingLink,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    NotUse = item.NotUse
                });
            }
            return destination;
        }
    }
}
