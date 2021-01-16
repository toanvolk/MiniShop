using AutoMapper;
using MiniShop.EF;
using MiniShop.Infrastructure;
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
                .AfterMap((source, destination) =>
                {
                    destination.Code = source.Name.URLFriendly();
                    destination.CreatedBy = "ADMIN";
                    destination.CreatedDate = DateTime.UtcNow;

                    destination.UpdatedBy = "ADMIN";
                    destination.UpdatedDate = DateTime.UtcNow;
                });

            //get
            CreateMap<Category, CategoryDto>();
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
                destination.Add(context.Mapper.Map<CategoryDto>(item));
                //destination.Add(new CategoryDto() { 
                //    Id = item.Id,
                //    Description = item.Description,
                //    Name = item.Name,
                //    Code = item.Name,
                //    ParentId = item.ParentId,
                //    NotUse = item.NotUse
                //});
            }
            return destination;
        }
    }
}
