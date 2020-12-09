using AutoMapper;
using Microsoft.Extensions.Options;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class BlogProfileMapping : Profile
    {
        public BlogProfileMapping()
        {
            ////create - update
            //CreateMap<BlogDto, Blog>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Description))
            //    .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(source => source.CategoryIds))
            //    .ForMember(dest => dest.AreaCode, opt => opt.MapFrom(source => source.AreaCode))
            //    .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price))
            //    .ForMember(dest => dest.TrackingLink, opt => opt.MapFrom(source => source.TrackingLink))
            //    .ForMember(dest => dest.Picture, opt => opt.MapFrom(source => source.Picture))
            //    .ForMember(dest => dest.SmallPicture, opt => opt.MapFrom(source => source.SmallPicture))
            //    .ForMember(dest => dest.BigPicture, opt => opt.MapFrom(source => source.BigPicture))
            //    .ForMember(dest => dest.Tag, opt => opt.MapFrom(source => source.Tag))
            //    .AfterMap((source, destination) =>
            //    {
            //        destination.Code = source.Name.URLFriendly();

            //        destination.CreatedBy = "ADMIN";
            //        destination.CreatedDate = DateTime.UtcNow;

            //        destination.UpdatedBy = "ADMIN";
            //        destination.UpdatedDate = DateTime.UtcNow;
            //    });

            ////get
            //CreateMap<Blog, BlogDto>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(source => source.Description))
            //    .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(source => source.CategoryIds))
            //    .ForMember(dest => dest.AreaCode, opt => opt.MapFrom(source => source.AreaCode))
            //    .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price))
            //    .ForMember(dest => dest.TrackingLink, opt => opt.MapFrom(source => source.TrackingLink))
            //    .ForMember(dest => dest.Picture, opt => opt.MapFrom(source => source.Picture))
            //    .ForMember(dest => dest.SmallPicture, opt => opt.MapFrom(source => source.SmallPicture))
            //    .ForMember(dest => dest.BigPicture, opt => opt.MapFrom(source => source.BigPicture))
            //    .ForMember(dest => dest.Tag, opt => opt.MapFrom(source => source.Tag));

            //load
            CreateMap<Blog, BlogDto>();


        }
    }
}
