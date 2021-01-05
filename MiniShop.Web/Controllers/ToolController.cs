using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniShop.App;

namespace MiniShop.Web.Controllers
{
    [Route("tool")]
    public class ToolController : BaseController
    {
        private readonly InfoServerConfig _infoServerConfig;
        private readonly IPostService _postService;
        private readonly IFeedbackService _feedbackService;

        public ToolController(IBaseService baseService, IBlogService blogService
            , IOptions<InfoServerConfig> optionAccessor
            , IPostService postService
            , IFeedbackService feedbackService
            ) : base(baseService)
        {
            _infoServerConfig = optionAccessor.Value;
            _postService = postService;
            _feedbackService = feedbackService;
        }
        [Route("posts")]
        public IActionResult Posts()
        {
            var postDtos = _postService.LoadData();
            var model = new Tuple<ICollection<PostDto>, InfoServerConfig>(postDtos.ToList(), _infoServerConfig);
            return View(model);
        }
        [Route("posts/feedback")]
        [HttpPost]
        public void FeedbackPosts(string name, string description)
        {
            var feedback = new FeedbackDto()
            {
                Name = name,
                Description = description,
                UserHostAddress = this.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            _feedbackService.Insert(feedback);
        }
    }
}
