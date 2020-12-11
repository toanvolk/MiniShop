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
        public BlogService(ILogger<BlogService> logger, IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
        }
        public ICollection<BlogDto> BlogMains()
        {
            var entities = _unitOfWorfk.BlogRepository.OrderByDescending(o => o.CreatedDate).Take(6).ToList();
            var blogDtos = new List<BlogDto>();
            entities.ForEach(o => blogDtos.Add(_mapper.Map<BlogDto>(o)));

            return blogDtos;            
        }
        public Tuple<ICollection<BlogDto>, int> GetDataAdmin(int page, int pageSize, ProductPageFilterDto paramSearch)
        {
            var query = _unitOfWorfk.BlogRepository.OrderByDescending(o => o.CreatedDate);
            var total = query.Count();
            var entities = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var dtos = new List<BlogDto>();

            entities.ForEach(o => dtos.Add(_mapper.Map<BlogDto>(o)));
            dtos.SetIndex(page, pageSize);

            return new Tuple<ICollection<BlogDto>, int>(dtos, total);
        }

        public bool Insert(BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);
            _unitOfWorfk.BlogRepository.Add(blog);

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Delete(Guid blogId)
        {
            _unitOfWorfk.BlogRepository.Delete(blogId);
            return _unitOfWorfk.SaveChanges() > 0;
        }
       
        public bool UpdateStatu(Guid blogId, bool ischecked)
        {
            var entity = _unitOfWorfk.BlogRepository.FindById(blogId);
            entity.UpdatedBy = "ADMIN";
            entity.UpdatedDate = DateTime.UtcNow;
            entity.NotUse = !ischecked;
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(BlogDto blogDto)
        {
            var entity = _mapper.Map<Blog>(blogDto);
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = "ADMIN";
            _unitOfWorfk.BlogRepository.Update(entity, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate", "NotUse");
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public BlogDto GetDataById(Guid blogId)
        {
            var entity = _unitOfWorfk.BlogRepository.FindById(blogId);
            return _mapper.Map<BlogDto>(entity);
        }

        public PageDataDto<BlogDto> LoadDataPage(PageFilterDto pageFilterDto)
        {
            var query = _unitOfWorfk.BlogRepository.OrderByDescending(o => o.CreatedDate);

            var entities = query.Skip(pageFilterDto.SkipCount).Take(pageFilterDto.TakeRecords).ToList();

            var blogDtos = new List<BlogDto>();
            entities.ForEach(o => blogDtos.Add(_mapper.Map<BlogDto>(o)));

            return new PageDataDto<BlogDto>(blogDtos, query.Count());
        }
    }
}
