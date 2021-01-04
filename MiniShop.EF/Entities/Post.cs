using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MiniShop.EF
{
    [Table("Post")]
    public class Post : AuditableEntity
    {
        [MaxLength(200)]
        public string FontName { get; set; }
        public string FontSign { get; set; }
        public PostType PostType { get; set; }

        [MaxLength(100)]
        public string Code { get; set; }
    }
    public enum PostType : byte
    {
        INPUT_TYPE,
        TEMPLATE_TYPE
    }
}
