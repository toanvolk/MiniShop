using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class ProductPageFilterDto
    {
        public string TextSearch { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
