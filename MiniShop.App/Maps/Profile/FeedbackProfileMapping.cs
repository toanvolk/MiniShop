using AutoMapper;
using MiniShop.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class FeedbackProfileMapping : Profile
    {
        public FeedbackProfileMapping()
        {
            //load
            CreateMap<FeedbackDto, Feedback>()
                .AfterMap((src, dest)=> {
                    dest.CreatedBy = "ADMIN";
                    dest.CreatedDate = DateTime.UtcNow;

                    dest.UpdatedBy = "ADMIN";
                    dest.UpdatedDate = DateTime.UtcNow;
                });
        }
    }
}
