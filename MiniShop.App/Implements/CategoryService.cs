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
    public class CategoryService : ICategoryService
    {
        private ILogger<CategoryService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        public CategoryService(ILogger<CategoryService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
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
            _unitOfWorfk.CategoryRepository.Update(category, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid categoryId)
        {
            _unitOfWorfk.CategoryRepository.Delete(categoryId);
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<CategoryDto> LoadData()
        {
            var datas = _unitOfWorfk.Categories.ToList();
            var model = _mapper.Map<List<CategoryDto>>(datas);

            return model;
        }

        public CategoryDto GetData(Guid categoryId)
        {
            var entity = _unitOfWorfk.CategoryRepository.FindById(categoryId);
            var categoryDto = _mapper.Map<CategoryDto>(entity);

            return categoryDto;
        }
    }
}
