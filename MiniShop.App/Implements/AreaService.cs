﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniShop.EF;
using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniShop.App
{
    public class AreaService : IAreaService
    {
        private ILogger<AreaService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public AreaService(ILogger<AreaService> logger, IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
        }     
        public ICollection<AreaDto> LoadData()
        {
            var datas = _unitOfWorfk.Areas.ToList();
            var model = _mapper.Map<List<AreaDto>>(datas);

            return model;
        }
    }
}
