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
    public class PostService : IPostService
    {
        private ILogger<PostService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        public PostService(ILogger<PostService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
        }

        public bool Create(PostDto postDto)
        {
            var entity = _mapper.Map<Post>(postDto);
            _unitOfWorfk.PostRepository.Add(entity);

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public bool Update(PostDto postDto)
        {
            var entity = _mapper.Map<Post>(postDto);
            _unitOfWorfk.PostRepository.Update(entity, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate");

            return _unitOfWorfk.SaveChanges() > 0;
        }

        public ICollection<PostDto> LoadData()
        {
            var entities = _unitOfWorfk.PostRepository.OrderByDescending(o => o.CreatedDate).ToList();
            var postDtos = new List<PostDto>();
            entities.ForEach(o => postDtos.Add(_mapper.Map<PostDto>(o)));

            return postDtos;
        }

        public bool Delete(Guid postId)
        {
            _unitOfWorfk.PostRepository.Delete(postId);
            return _unitOfWorfk.SaveChanges() > 0;
        }

        public PostDto GetDataById(Guid postId)
        {
            var entity = _unitOfWorfk.PostRepository.FindById(postId);
            PostDto postDto = _mapper.Map<PostDto>(entity);

            return postDto;
        }
    }
}
