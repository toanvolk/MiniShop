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
        ICollection<CategoryProductDto> LoadDataPageDefault(int take);
        Tuple<ICollection<ProductDto>, int> LoadDataPageAdmin(int page, int pageSize, ProductPageFilterDto paramSearch);
        ProductDto GetData(Guid productId);
        bool UpdateStatu(Guid productId, bool ischecked);
        ICollection<CategoryDto> GetCategories();
        ICollection<CategoryDto> GetCategoryForProductModify();
        ICollection<AreaDto> GetAreas();
        ICollection<ProductDto> LoadDataHero();
        bool UpdateHero(Guid productId, bool ischecked, string userName);
        ICollection<string> TagList();
        ProductDto LoadDataView(Guid productId);
        Guid GetProductId(string code);
        ICollection<ProductDto> GetForAdsense(int take, string category);
        ICollection<ProductDto> GetDataBySearchString(string searchString, string sort);
    }
}
