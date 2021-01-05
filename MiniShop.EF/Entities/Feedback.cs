using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MiniShop.EF
{
    [Table("Feedback")]
    public class Feedback : AuditableEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string UserHostAddress { get; set; }
        public string Description { get; set; }
    }
}
