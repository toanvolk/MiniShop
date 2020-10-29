using AutoMapper;
using MiniShop.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class CategoryProfileMapping : Profile
    {
        public CategoryProfileMapping()
        {
            //create - update
            CreateMap<CategoryDto, Category>()
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
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Description));
            //load
            CreateMap<List<Category>, List<CategoryDto>>().ConvertUsing<CategoryTypingConvert>();
        }
    }
    public class CategoryTypingConvert : ITypeConverter<List<Category>, List<CategoryDto>>
    {
        public List<CategoryDto> Convert(List<Category> source, List<CategoryDto> destination, ResolutionContext context)
        {
            destination ??= new List<CategoryDto>();
            foreach (var item in source)
            {
                destination.Add(new CategoryDto() { 
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
