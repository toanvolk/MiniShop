using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{   
    public class MetaTagDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public FacebookMetaTag FacebookMeta { get; set; }
        public TwitterMetaTag TwitterMeta { get; set; }
    }
    public class FacebookMetaTag
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string SiteName { get; set; }
        public string Description { get; set; }
    }
    public class TwitterMetaTag
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Card { get; set; }
    }
}
