using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.App
{
    public interface IProductService
    {
        bool Insert(ProductDto data);
        bool Update(ProductDto data);
        bool Delete(Guid productId);
        Tuple<ICollection<ProductDto>, int> LoadDataPage(int page, int pageSize, ProductPageFilterDto filter);
        Tuple<ICollection<ProductDto>, int> LoadDataPageAdmin(int page, int pageSize, ProductPageFilterDto paramSearch);
        ProductDto GetData(Guid productId);
        bool UpdateStatu(Guid productId, bool ischecked);
        ICollection<CategoryDto> GetCategories();
        ICollection<AreaDto> GetAreas();
        ICollection<ProductDto> LoadDataHero();
        bool UpdateHero(Guid productId, bool ischecked, string userName);
        ICollection<string> TagList();
        ProductDto LoadDataView(Guid productId);
        Guid GetProductId(string code);
    }
}
