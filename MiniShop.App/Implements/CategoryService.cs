using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniShop.App
{
    public class CategoryService : ICategoryService
    {
        private ILogger<CategoryService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly InfoServerConfig _infoServerConfig;
        public CategoryService(ILogger<CategoryService> logger, IUnitOfWork unitOfWork, IMapper mapper, IOptions<InfoServerConfig> optionAccessor)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _infoServerConfig = optionAccessor.Value;
        }        
        public bool Insert(CategoryDto data)
        {
            var category = _mapper.Map<Category>(data);
            _unitOfWorfk.CategoryRepository.Add(category);

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(CategoryDto data)
        {
            var category = _mapper.Map<Category>(data);
            _unitOfWorfk.CategoryRepository.Update(category, AccessPropertyMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse", "Products");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid categoryId)
        {
            _unitOfWorfk.CategoryRepository.Delete(categoryId);
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<CategoryDto> LoadDataAdmin()
        {
            var datas = _unitOfWorfk.Categories.OrderBy(o=>o.SortIndex).ToList();
            var model = _mapper.Map<List<CategoryDto>>(datas);
            model.SetIndex();

            return model;
        }
        public ICollection<CategoryDto> LoadData()
        {
            var datas = _unitOfWorfk.Categories.Where(o=>o.NotUse != true).ToList();
            var model = _mapper.Map<List<CategoryDto>>(datas);
            model.SetIndex();

            return model;
        }
        public CategoryDto GetData(Guid categoryId)
        {
            var entity = _unitOfWorfk.CategoryRepository.FindById(categoryId);
            var categoryDto = _mapper.Map<CategoryDto>(entity);

            return categoryDto;
        }

        public bool UpdateStatu(Guid categoryId, bool ischecked)
        {
            var entity = _unitOfWorfk.CategoryRepository.FindById(categoryId);
            entity.UpdatedBy = "ADMIN";
            entity.UpdatedDate = DateTime.UtcNow;
            entity.NotUse = !ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public CategoryProductDto GetDataByCode(string code, string sort)
        {
            var entity = _unitOfWorfk.CategoryRepository.Filter(o => o.NotUse != true && o.Code == code).FirstOrDefault();
            var dto = _mapper.Map<CategoryDto>(entity);
            var products = new List<ProductDto>();

            var categorys = _unitOfWorfk.CategoryRepository.Filter(o=> o.NotUse != true && o.ParentId == dto.Id, p => p.Products).ToList();
            foreach (var category in categorys)
            {
                var productDtos = _mapper.Map<List<ProductDto>>(category.Products);                
                productDtos.ForEach(o =>
                {
                    o.Picture = $"{_infoServerConfig.FileRootPath}/{o.Picture}";
                    o.Description = o.Description;//?.TakeWords(10);
                    o.Code = $"/san-pham/{o.Code}";
                });

                products.AddRange(productDtos);
                
            }
            Enum.TryParse(sort?.ToUpper(), out ProductSortEnum productSortEnum);
            products = products.Sort(productSortEnum);

            return new CategoryProductDto()
            {
                Category = dto,
                Products = products
            };
        }

        public ICollection<CategoryDto> LoadDataNonRoot()
        {
            var datas = _unitOfWorfk.Categories.Where(o => o.NotUse != true && o.ParentId != null).OrderByDescending(o=>o.CreatedDate).ToList();
            var model = _mapper.Map<List<CategoryDto>>(datas);
            model.SetIndex();

            return model;
        }
    }
}
