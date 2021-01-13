using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class ProductDto : IndexDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string SmallPicture { get; set; }
        public string BigPicture { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        //public Guid? CategoryId { get; set; }
        public bool NotUse { get; set; }
        public string CategoryName { get; set; }
        public string TrackingLink { get; set; }
        public string AreaCode { get; set; }
        public bool IsHero { get; set; }
        public TagEnum Tag { get; set; }
        public decimal PriceIgnore { get; set; }
        public bool? IsRedirectToPageRoot { get; set; }
        public virtual CategoryDto CategoryDto { get; set; }

    }
}
