using System;

namespace MiniShop.EF
{
    public class Blog : AuditableEntity
    {      
        public string Content { get; set; }
        public string DescriptionShort { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string HashTag { get; set; }
        public string PicturePath { get; set; }
        public string ReadMorePath { get; set; }
        public bool NotUse { get; set; }
    }
}
