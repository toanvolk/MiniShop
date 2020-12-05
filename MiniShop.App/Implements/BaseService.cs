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
    public class BaseService : IBaseService
    {
        private ILogger<AreaService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public BaseService(ILogger<AreaService> logger, IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
        }   
        public void CountClick(string userHostAddress, string url, string keyView)
        {
            var entity = new TouchHistory()
            {
                UserHostAddress = userHostAddress,
                CreatedDate = DateTime.UtcNow,
                Url = url,
                KeyView = keyView,
                Id = Guid.NewGuid()
            };

            _unitOfWorfk.TouchHistorys.Add(entity);
            _unitOfWorfk.SaveChanges();
        }
    }
}
