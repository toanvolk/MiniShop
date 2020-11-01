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
    public class ProductService : IProductService
    {
        private ILogger<ProductService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAreaService _areaService;
        public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper
            , ICategoryService categoryService
            ,IAreaService areaService)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
            _areaService = areaService;
        }        
        public bool Insert(ProductDto data)
        {
            var product = _mapper.Map<Product>(data);
            _unitOfWorfk.ProductRepository.Add(product);

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(ProductDto data)
        {
            var product = _mapper.Map<Product>(data);
            _unitOfWorfk.ProductRepository.Update(product, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid productId)
        {
            _unitOfWorfk.ProductRepository.Delete(productId);
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<ProductDto> LoadData()
        {
            var datas = _unitOfWorfk.Products.ToList();
            var model = _mapper.Map<List<ProductDto>>(datas);

            return model;
        }

        public ProductDto GetData(Guid productId)
        {
            var entity = _unitOfWorfk.ProductRepository.FindById(productId);
            var productDto = _mapper.Map<ProductDto>(entity);

            return productDto;
        }

        public bool UpdateStatu(Guid productId, bool ischecked)
        {
            var entity = _unitOfWorfk.ProductRepository.FindById(productId);
            entity.UpdatedBy = "ADMIN";
            entity.UpdatedDate = DateTime.Now;
            entity.NotUse = !ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<CategoryDto> GetCategories()
        {
            return _categoryService.LoadData();
        }
        public ICollection<AreaDto> GetAreas()
        {
            return _areaService.LoadData();
        }
    }
}
