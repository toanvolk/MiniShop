using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class BlogDto
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string HashTag { get; set; }
        public string PicturePath { get; set; }
    }
}
