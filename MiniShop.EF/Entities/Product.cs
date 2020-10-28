using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniShop.EF
{
    [Table("Product")]
    public class Product : AuditableEntity
    {
        [MaxLength(100)]
        public string Code { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Picture { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool NotUse { get; set; }
        public Guid CategoryId { get; set; }
    }
}
