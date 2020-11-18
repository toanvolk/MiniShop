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
    public class ProductService : IProductService
    {
        private ILogger<ProductService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAreaService _areaService;
        private readonly string _fileRootPath = "/shared/UserFiles/Folders";
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
            _unitOfWorfk.ProductRepository.Update(product, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse", "IsHero");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid productId)
        {
            _unitOfWorfk.ProductRepository.Delete(productId);
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<ProductDto> LoadData()
        {           
            var query = _unitOfWorfk.Products
                .Join(_unitOfWorfk.ProductCategories, p => p.Id, pc => pc.ProductId, (p, pc) => new { p, pc })
                .Join(_unitOfWorfk.Categories, ppc => ppc.pc.CategoryId, c => c.Id, (ppc, c) => new { ppc, c })
                .Join(_unitOfWorfk.Areas, cppc => cppc.ppc.p.AreaCode, a => a.Code, (cppc, a) => new { cppc, a })
                .Select( m=> new ProductDto()
                {
                    Id = m.cppc.ppc.p.Id,
                    Name = m.cppc.ppc.p.Name,
                    Description = m.cppc.ppc.p.Description,
                    AreaCode = m.cppc.ppc.p.AreaCode,
                    Price = m.cppc.ppc.p.Price,
                    TrackingLink = m.cppc.ppc.p.TrackingLink,
                    Picture = m.cppc.ppc.p.Picture,
                    NotUse = m.cppc.ppc.p.NotUse,
                    IsHero = m.cppc.ppc.p.IsHero,
                    CategoryName = m.cppc.c.Name
                });            

            var model = query.ToList();
            return model;
        }
        public Tuple<ICollection<ProductDto>, int> LoadDataPage(int page, int pageSize, ProductPageFilterDto filter)
        {
            var query = _unitOfWorfk.Products
                .Join(_unitOfWorfk.ProductCategories, p => p.Id, pc => pc.ProductId, (p, pc) => new { p, pc })
                .Join(_unitOfWorfk.Categories, ppc => ppc.pc.CategoryId, c => c.Id, (ppc, c) => new { ppc, c })
                .Join(_unitOfWorfk.Areas, cppc => cppc.ppc.p.AreaCode, a => a.Code, (cppc, a) => new { cppc, a })
                .Select(m => new ProductDto()
                {
                    Id = m.cppc.ppc.p.Id,
                    Name = m.cppc.ppc.p.Name,
                    Description = m.cppc.ppc.p.Description,
                    AreaCode = m.cppc.ppc.p.AreaCode,
                    Price = m.cppc.ppc.p.Price,
                    TrackingLink = m.cppc.ppc.p.TrackingLink,
                    Picture = m.cppc.ppc.p.Picture,
                    NotUse = m.cppc.ppc.p.NotUse,
                    IsHero = m.cppc.ppc.p.IsHero,
                    CategoryName = m.cppc.c.Name,
                    Tag = (TagEnum) m.cppc.ppc.p.Tag
                });            

            var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var total = query.Count();

            return new Tuple<ICollection<ProductDto>, int>(model, total);
        }
        public Tuple<ICollection<ProductDto>, int> LoadDataPageAdmin(int page, int pageSize, ProductPageFilterDto paramSearch)
        {
            var query = _unitOfWorfk.Products
                .Join(_unitOfWorfk.ProductCategories, p => p.Id, pc => pc.ProductId, (p, pc) => new { p, pc })
                .Join(_unitOfWorfk.Categories, ppc => ppc.pc.CategoryId, c => c.Id, (ppc, c) => new { ppc, c })
                .Join(_unitOfWorfk.Areas, cppc => cppc.ppc.p.AreaCode, a => a.Code, (cppc, a) => new { cppc, a })
                .Select(m => new ProductDto()
                {
                    Id = m.cppc.ppc.p.Id,
                    Name = m.cppc.ppc.p.Name,
                    Description = m.cppc.ppc.p.Description,
                    AreaCode = m.cppc.ppc.p.AreaCode,
                    Price = m.cppc.ppc.p.Price,
                    TrackingLink = m.cppc.ppc.p.TrackingLink,
                    Picture = m.cppc.ppc.p.Picture,
                    NotUse = m.cppc.ppc.p.NotUse,
                    IsHero = m.cppc.ppc.p.IsHero,
                    CategoryName = m.cppc.c.Name,
                    Tag = (TagEnum)m.cppc.ppc.p.Tag
                });

            var model = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var total = query.Count();

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

        public ICollection<ProductDto> LoadDataHero()
        {
            var products = _unitOfWorfk.ProductRepository.Filter(o => o.IsHero);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            foreach (var item in productDtos)
            {
                item.BigPicture = $"{_fileRootPath}/{item.Picture}";
            }
            return productDtos;
        }
        public bool UpdateHero(Guid productId, bool ischecked, string userName)
        {
            var entity = _unitOfWorfk.ProductRepository.FindById(productId);
            entity.UpdatedBy = userName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsHero = ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public int CountClick(Guid productId, string userHostAddress, string userHostName)
        {
            var entity = new TouchHistory() {
                ProductId = productId,
                UserHostAddress = userHostAddress,
                CreatedBy = userHostName,
                CreatedDate = DateTime.Now,
                Id = Guid.NewGuid()
            };

            _unitOfWorfk.TouchHistorys.Add(entity);
            return _unitOfWorfk.SaveChanges();
        }

        public ICollection<string> TagList()
        {
            var list = Enum.GetNames(typeof(TagEnum));
            return list;
        }
    }
}
