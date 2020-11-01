using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IAreaService
    {
      
        ICollection<AreaDto> LoadData();
    }
}
