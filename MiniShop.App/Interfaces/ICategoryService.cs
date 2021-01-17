using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface ICategoryService
    {
        bool Insert(CategoryDto data);
        bool Update(CategoryDto data);
        bool Delete(Guid categoryId);
        ICollection<CategoryDto> LoadData();
        ICollection<CategoryDto> LoadDataAdmin();
        CategoryDto GetData(Guid categoryId);
        CategoryProductDto GetDataByCode(string code);
        bool UpdateStatu(Guid categoryId, bool ischecked);
        ICollection<CategoryDto> LoadDataNonRoot();
    }
}
