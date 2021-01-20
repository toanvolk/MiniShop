using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniShop.App
{
    public static class ProductExtension
    {
        public static List<ProductDto> Sort(this List<ProductDto> products, ProductSortEnum productSortEnum)
        {
            if (productSortEnum == ProductSortEnum.COMMON)
                products = products.OrderByDescending(o => o.Tag).ToList();
            else
            {
                if (productSortEnum == ProductSortEnum.DECREASE_PRICE)
                    products = products.OrderByDescending(o => o.Price).ToList();
                if (productSortEnum == ProductSortEnum.INCREASE_PRICE)
                    products = products.OrderBy(o => o.Price).ToList();
            }
            return products;
        }
    }
}
