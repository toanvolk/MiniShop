using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public bool NotUse { get; set; }
        public Guid CategoryId { get; set; }
        public string TrackingLink { get; set; }
    }
}
