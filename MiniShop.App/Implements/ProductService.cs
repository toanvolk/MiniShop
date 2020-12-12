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
            _unitOfWorfk.ProductRepository.Add(product);

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(ProductDto data)
        {
            var product = _mapper.Map<Product>(data);
            _unitOfWorfk.ProductRepository.Update(product, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse", "IsHero");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid productId)
        {
            _unitOfWorfk.ProductRepository.Delete(productId);
            return _unitOfWorfk.SaveChanges() > 0;
        }
        public Tuple<ICollection<ProductDto>, int> LoadDataPage(int page, int pageSize, ProductPageFilterDto filter)
        {

            IQueryable<Product> query = null;
            if (filter.CategoryIds.Count > 0)
            {
                foreach (var item in filter.CategoryIds)
                {
                    var queryChild = _unitOfWorfk.Products.Where(o => o.CategoryIds.Contains(item.ToString()));
                    if (query == null)
                        query = queryChild;
                    else
                        query = query.Union(queryChild);
                }

            }
            else
            {
                query = _unitOfWorfk.Products;
            }
            if (!string.IsNullOrWhiteSpace(filter.TextSearch))
            {
                query = query.Where(o => o.Name.Contains(filter.TextSearch));

            }
            var queryDto = query.Where(o=> o.NotUse != true).OrderByDescending(o => o.Tag).Select(m => new ProductDto()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                AreaCode = m.AreaCode,
                Price = m.Price,
                TrackingLink = m.TrackingLink,
                Picture = $"{_infoServerConfig.FileRootPath}/{m.Picture}",
                Code = m.Code,
                NotUse = m.NotUse,
                IsHero = m.IsHero,
                Tag = (TagEnum)m.Tag
            });

            //var model = queryDto.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var model = queryDto.Skip(filter.SkipCount).Take(filter.TakeRecords).ToList();
            var total = queryDto.Count();

            return new Tuple<ICollection<ProductDto>, int>(model, total);
        }
        private bool filterCategoryId(Product product, List<Guid> filters)
        {
            var categorys = product.CategoryIds?.Split(',');
            if (categorys == null) return false;

            return filters.Any(o => categorys.Any(c => c == o.ToString()));
        }
        private IQueryable<Guid> ConvertGuid(string categorys)
        {
            if (string.IsNullOrWhiteSpace(categorys)) return new List<Guid>().AsQueryable();
            var list = categorys.Split(',');
            var guids = new List<Guid>();
            foreach (var item in list)
            {
                guids.Add(Guid.Parse(item));
            }
            return guids.AsQueryable();
        }
        public Tuple<ICollection<ProductDto>, int> LoadDataPageAdmin(int page, int pageSize, ProductPageFilterDto paramSearch)
        {

            IQueryable<Product> query = null;
            if (paramSearch.CategoryIds != null && paramSearch.CategoryIds.Count > 0)
            {
                foreach (var item in paramSearch.CategoryIds)
                {
                    var queryChild = _unitOfWorfk.Products.Where(o => o.CategoryIds.Contains(item.ToString()));
                    if (query == null)
                        query = queryChild;
                    else
                        query = query.Union(queryChild);
                }

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
            var entities = _unitOfWorfk.ProductRepository.OrderByDescending(o => o.CreatedDate).Take(take).ToList();
            var productDtos = new List<ProductDto>();

            entities.ForEach(o => productDtos.Add(_mapper.Map<ProductDto>(o)));
            return productDtos;
        }
    }
}
