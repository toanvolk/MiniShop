using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IBlogService
    {
      
        ICollection<BlogDto> BlogMains();
        Tuple<ICollection<BlogDto>, int> GetDataAdmin(int page, int pageSize, ProductPageFilterDto paramSearch);
        bool Insert(BlogDto blogDto);
        bool Delete(Guid blogId);
        bool UpdateStatu(Guid blogId, bool ischecked);
        bool Update(BlogDto blogDto);
        BlogDto GetDataById(Guid blogId);
        PageDataDto<BlogDto> LoadDataPage(PageFilterDto pageFilterDto);
    }
}
