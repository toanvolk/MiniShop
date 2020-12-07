using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IBlogService
    {
      
        ICollection<BlogDto> BlogMains();
    }
}
