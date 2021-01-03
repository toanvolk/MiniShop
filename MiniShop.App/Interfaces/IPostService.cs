using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IPostService
    {
      
        bool Create(PostDto postDto);
        bool Update(PostDto postDto);
        ICollection<PostDto> LoadData();
        bool Delete(Guid postId);
        PostDto GetDataById(Guid postId);
    }
}
