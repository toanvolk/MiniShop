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
    public class FeedbackService : IFeedbackService
    {
        private ILogger<FeedbackService> _logger { get; set; }
        private readonly IUnitOfWork _unitOfWorfk;
        private readonly IMapper _mapper;
        public FeedbackService(ILogger<FeedbackService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfk = unitOfWork;
            _mapper = mapper;
        }     

        public bool Insert(FeedbackDto feedbackDto)
        {
            var entity = _mapper.Map<Feedback>(feedbackDto);
            _unitOfWorfk.FeedbackRepository.Add(entity);

            return _unitOfWorfk.SaveChanges() > 0;
        }
    }
}
