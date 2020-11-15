using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.App
{
    public class HomeService : IHomeService
    {
        private ILogger<HomeService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public HomeService(ILogger<HomeService> logger, IUnitOfWork unitOfWork, IMapper mapper
            , IProductService productService) 
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _productService = productService;
        }

        public ICollection<ProductReviewDto> GetProductReview(DateTime fromDate, DateTime toDate)
        {
            var results = new List<ProductReviewDto>();
            var fromDateParam = new Microsoft.Data.SqlClient.SqlParameter("@p0", fromDate);
            fromDateParam.DbType = System.Data.DbType.DateTime2;

            var toDateParam = new Microsoft.Data.SqlClient.SqlParameter("@p1", toDate);
            toDateParam.DbType = System.Data.DbType.DateTime2;

            var datas = _unitOfWorfk.GetDynamicResult("sp_mnshop_getproductreview @p0, @p1", fromDateParam, toDateParam);
            foreach (var item in datas)
            {
                results.Add(new ProductReviewDto() { 
                    ProductName = item.ProductName,
                    Count = item.Count 
                });
            }

            return results;
        }
    }
}
