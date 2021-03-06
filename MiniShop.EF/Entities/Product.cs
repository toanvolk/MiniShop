﻿using System;
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
        [MaxLength(500)]
        public string SmallPicture { get; set; }
        [MaxLength(500)]
        public string BigPicture { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool NotUse { get; set; }
        public string TrackingLink { get; set; }
        [MaxLength(50)]
        public string AreaCode { get; set; }
        public bool IsHero { get; set; }
        public int Tag { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal PriceIgnore { get; set; }
        public bool? IsRedirectToPageRoot { get; set; }
        

        [ReferenceCustom]
        public virtual Category Category { get; set; }
    }
}
