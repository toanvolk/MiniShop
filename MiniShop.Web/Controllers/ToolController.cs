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

        public ToolController(IBaseService baseService, IBlogService blogService
            , IOptions<InfoServerConfig> optionAccessor
            , IProductService productService, IPostService postService) : base(baseService)
        {
            _infoServerConfig = optionAccessor.Value;
            _postService = postService;
        }
        [Route("posts")]
        public IActionResult Posts()
        {
            var postDtos = _postService.LoadData();
            var model = new Tuple<ICollection<PostDto>>(postDtos.ToList());
            return View(model);
        }       
    }
}
