using AutoMapper;
using MiniShop.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class AreaProfileMapping : Profile
    {
        public AreaProfileMapping()
        {
            //load
            CreateMap<List<Area>, List<AreaDto>>().ConvertUsing<AreaTypingConvert>();
        }
    }
    public class AreaTypingConvert : ITypeConverter<List<Area>, List<AreaDto>>
    {
        public List<AreaDto> Convert(List<Area> source, List<AreaDto> destination, ResolutionContext context)
        {
            destination ??= new List<AreaDto>();
            foreach (var item in source)
            {
                destination.Add(new AreaDto() { Code = item.Code, Name = item.Name });
            }
            return destination;
        }
    }
}
