using AutoMapper;
using MiniShop.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class PostProfileMapping : Profile
    {
        public PostProfileMapping()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>().AfterMap((source, destination) =>
            {

                destination.CreatedBy = "ADMIN";
                destination.CreatedDate = DateTime.UtcNow;

                destination.UpdatedBy = "ADMIN";
                destination.UpdatedDate = DateTime.UtcNow;
            }); ;
        }
    }
}
