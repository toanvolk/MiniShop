using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IProductService
    {
        bool Insert(ProductDto data);
        bool Update(ProductDto data);
        bool Delete(Guid productId);
        ICollection<ProductDto> LoadData();
        ProductDto GetData(Guid productId);
        bool UpdateStatu(Guid productId, bool ischecked);
        ICollection<CategoryDto> GetCategories();
        ICollection<AreaDto> GetAreas();
    }
}
