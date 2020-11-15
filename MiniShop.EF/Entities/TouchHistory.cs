using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MiniShop.EF
{
    [Table("TouchHistory")]
    public class TouchHistory : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public string UserHostAddress { get; set; }
    }
}
