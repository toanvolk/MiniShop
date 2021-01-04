using MiniShop.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string FontName { get; set; }
        public string Code { get; set; }
        public string FontSign { get; set; }
        public PostType PostType { get; set; }
    }
}
