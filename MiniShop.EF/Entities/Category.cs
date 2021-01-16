using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MiniShop.EF
{
    public class Category : AuditableEntity
    {
        [MaxLength(200)]
        public string Code { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public bool NotUse { get; set; }

        [CollectionCustom]
        public virtual ICollection<Product> Products { get; set; }
    }
}
