using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MiniShop.EF
{
    [Table("Area")]
    public class Area : AuditableEntity
    {
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
