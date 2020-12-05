using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MiniShop.EF
{
    [Table("TouchHistory")]
    public class TouchHistory : AuditableEntity
    {
        public string UserHostAddress { get; set; }
        public string Url { get; set; }
        public string KeyView { get; set; }
    }
}
