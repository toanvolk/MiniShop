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
        Tuple<ICollection<ProductDto>, int> LoadDataPage(int page, int pageSize, string textSearch= null);
        ProductDto GetData(Guid productId);
        bool UpdateStatu(Guid productId, bool ischecked);
        ICollection<CategoryDto> GetCategories();
        ICollection<AreaDto> GetAreas();
        ICollection<ProductDto> LoadDataHero();
        bool UpdateHero(Guid productId, bool ischecked, string userName);
    }
}
