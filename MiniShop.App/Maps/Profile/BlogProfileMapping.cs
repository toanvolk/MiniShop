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
        TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        public BlogProfileMapping()
        {
            ////create - update
            CreateMap<BlogDto, Blog>()
                .AfterMap((src, dst)=> {
                    dst.ReadMorePath = $"blog/{src.Title.URLFriendly()}";
                    dst.CreatedBy = "ADMIN";
                    dst.CreatedDate = DateTime.UtcNow;

                    dst.UpdatedBy = "ADMIN";
                    dst.UpdatedDate = DateTime.UtcNow;

                    dst.PublishDate = src.PublishDate.ToUniversalTime();
                });

           //load
            CreateMap<Blog, BlogDto>()
                .AfterMap((src, dst) => {                    
                    dst.PublishDate = src.PublishDate.ToUniversalTime();
                    dst.PublishDate = TimeZoneInfo.ConvertTimeFromUtc(src.PublishDate, tst);
                });


        }
    }
}
