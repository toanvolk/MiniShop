using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniShop.App
{
    public class BlogService : IBlogService
    {
        private ILogger<BlogService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public BlogService(ILogger<BlogService> logger, IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
        }     
        public ICollection<BlogDto> BlogMains()
        {
            var list = new List<BlogDto>() { 
                new BlogDto()
                {
                    Title = "Hôm nay tin gì",
                    Author = "ABC",
                    Category = "Xã hội",
                    Description = "Thời tiết hơi lạnh, bụi nhiều, không khí ô nhiễm...",
                    HashTag = "o_nhiem, khong_khi",
                    PicturePath = "https://storage.googleapis.com/chydlx/codepen/blog-cards/image-1.jpg",
                    PublishDate = DateTime.Now
                },
                new BlogDto()
                {
                    Title = "Hôm nay tin gì",
                    Author = "ABC",
                    Category = "Xã hội",
                    Description = "Thời tiết hơi lạnh, bụi nhiều, không khí ô nhiễm...",
                    HashTag = "o_nhiem, khong_khi",
                    PicturePath = "https://storage.googleapis.com/chydlx/codepen/blog-cards/image-1.jpg",
                    PublishDate = DateTime.Now
                },
                new BlogDto()
                {
                    Title = "Hôm nay tin gì",
                    Author = "ABC",
                    Category = "Xã hội",
                    Description = "Thời tiết hơi lạnh, bụi nhiều, không khí ô nhiễm...",
                    HashTag = "o_nhiem, khong_khi",
                    PicturePath = "https://storage.googleapis.com/chydlx/codepen/blog-cards/image-1.jpg",
                    PublishDate = DateTime.Now
                },
            };

            return list;
        }
    }
}
