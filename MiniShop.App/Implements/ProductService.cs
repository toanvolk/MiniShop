using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public class ProductService : IProductService
    {
        private ILogger<ProductService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAreaService _areaService;
        private readonly InfoServerConfig _infoServerConfig;
        public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper
            , ICategoryService categoryService
            , IAreaService areaService
            , IOptions<InfoServerConfig> optionAccessor)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
            _areaService = areaService;
            _infoServerConfig = optionAccessor.Value;
        }
        public bool Insert(ProductDto data)
        {
            var product = _mapper.Map<Product>(data);
            //_unitOfWorfk.ProductRepository.Add(product, product.Category);
            _unitOfWorfk.ProductCategoryRepository.Add(product, product.Category);
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(ProductDto data)
        {
            var product = _mapper.Map<Product>(data);
            _unitOfWorfk.ProductRepository.Update(product, AccessPropertyMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse", "IsHero");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid productId)
        {
            _unitOfWorfk.ProductRepository.Delete(productId);
            return _unitOfWorfk.SaveChanges() > 0;
        }
        public Tuple<ICollection<ProductDto>, int> LoadDataPageAdmin(int page, int pageSize, ProductPageFilterDto paramSearch)
        {
            IQueryable<Product> query = null;
            if (paramSearch.CategoryIds != null && paramSearch.CategoryIds.Count > 0)
            {                
                var queryChild = _unitOfWorfk.Products.Where(o => paramSearch.CategoryIds.Contains(o.Category.Id));
                if (query == null)
                    query = queryChild;
                else
                    query = query.Union(queryChild);
            }
            else
            {
                query = _unitOfWorfk.Products;
            }
            if (!string.IsNullOrWhiteSpace(paramSearch.TextSearch))
            {
                query = query.Where(o => o.Name.Contains(paramSearch.TextSearch));

            }
            var queryDto = query
                .OrderByDescending(o => o.CreatedDate)
                .Select(m => new ProductDto()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    AreaCode = m.AreaCode,
                    Price = m.Price,
                    TrackingLink = m.TrackingLink,
                    Picture = $"{_infoServerConfig.FileRootPath}/{m.Picture}",
                    NotUse = m.NotUse,
                    IsHero = m.IsHero,
                    Tag = (TagEnum)m.Tag
                });
            var model = queryDto.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var total = queryDto.Count();
            model.SetIndex(page, pageSize);

            return new Tuple<ICollection<ProductDto>, int>(model, total);
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
            entity.UpdatedDate = DateTime.UtcNow;
            entity.NotUse = !ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<CategoryDto> GetCategories()
        {
            return _categoryService.LoadDataNonRoot();
        }
        public ICollection<CategoryDto> GetCategoryForProductModify()
        {
            return _categoryService.LoadData();
        }
        public ICollection<AreaDto> GetAreas()
        {
            return _areaService.LoadData();
        }

        public ICollection<ProductDto> LoadDataHero()
        {
            var products = _unitOfWorfk.ProductRepository.Filter(o => o.IsHero);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            foreach (var item in productDtos)
            {
                item.BigPicture = $"{_infoServerConfig.FileRootPath}/{item.Picture}";
            }
            return productDtos;
        }
        public bool UpdateHero(Guid productId, bool ischecked, string userName)
        {
            var entity = _unitOfWorfk.ProductRepository.FindById(productId);
            entity.UpdatedBy = userName;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.IsHero = ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }
        public ICollection<string> TagList()
        {
            var list = Enum.GetNames(typeof(TagEnum));
            return list;
        }

        public ProductDto LoadDataView(Guid productId)
        {
            var products = _unitOfWorfk.ProductRepository.Filter(o => o.Id == productId).FirstOrDefault();
            var productDto = _mapper.Map<ProductDto>(products);
            productDto.BigPicture = $"{_infoServerConfig.FileRootPath}/{productDto.Picture}";

            return productDto;
        }

        public Guid GetProductId(string code)
        {
            var productDto = _unitOfWorfk.ProductRepository.Filter(o => o.Code == code).FirstOrDefault();
            productDto ??= new Product();
            return productDto.Id;
        }

        public ICollection<ProductDto> GetForAdsense(int take, string category)
        {
            
            var entities = _unitOfWorfk.ProductRepository.OrderByDescending(o => o.CreatedDate).ToList();
            var productDtos = new List<ProductDto>();
            entities.ForEach(o => productDtos.Add(_mapper.Map<ProductDto>(o)));

            var productDtoReturns = new List<ProductDto>();
            if (string.IsNullOrEmpty(category))
            {
                productDtoReturns.AddRange(productDtos);
            }
            else
            {
                //TODO: need fix CategoryIds
                //foreach (var item in productDtos)
                //{
                //    if (!string.IsNullOrEmpty(item.CategoryIds))
                //    {
                //        var categorys = item.CategoryIds.Split(',');
                //        var categorySrc = category.Split(',');
                //        if (categorys.Any(o => categorySrc.Contains(o)))
                //        {
                //            productDtoReturns.Add(item);
                //        }
                //    }
                //}
            }
            return productDtoReturns.Take(2).ToList();
        }

        public ICollection<CategoryProductDto> LoadDataPageDefault(int take)
        {
            var categoryProducts = new List<CategoryProductDto>();
            //load category
            var categorys = _unitOfWorfk.CategoryRepository.Filter(o => o.NotUse != true && o.ParentId == null).OrderBy(o=>o.SortIndex).ToList();
            foreach (var category in categorys)
            {
                var categoryChild = _unitOfWorfk.CategoryRepository.Filter(o => o.ParentId == category.Id, p=>p.Products).ToList();
                var categoryDto = _mapper.Map<CategoryDto>(category);
                var productDtos = new List<ProductDto>();
                foreach (var item in categoryChild)
                {
                    var products = item.Products.Where(o => o.NotUse != true).OrderByDescending(o => o.CreatedDate).Take(take).ToList();
                    productDtos.AddRange(_mapper.Map<List<ProductDto>>(products));
                }

                productDtos.ForEach(o =>
                {
                    o.Picture = $"{_infoServerConfig.FileRootPath}/{o.Picture}";
                    o.Description = o.Description;//?.TakeWords(10);
                    o.Code = $"/san-pham/{o.Code}";
                });

                categoryProducts.Add(new CategoryProductDto()
                {
                    Category = categoryDto,
                    Products = productDtos
                });
            }

            return categoryProducts;
        }

        public ICollection<ProductDto> GetDataBySearchString(string searchString)
        {
            var entities = _unitOfWorfk.ProductRepository
                .Filter(o => o.NotUse != true && o.Name.Contains(searchString), p=>p.Category)
                .ToList();
            var productDtos = new List<ProductDto>();

            entities.ForEach(o => productDtos.Add(_mapper.Map<ProductDto>(o)));

            productDtos.ForEach(o =>
            {
                o.Picture = $"{_infoServerConfig.FileRootPath}/{o.Picture}";
                o.Description = o.Description;//?.TakeWords(10);
                o.Code = $"/san-pham/{o.Code}";
            });

            return productDtos;
        }
    }
}
