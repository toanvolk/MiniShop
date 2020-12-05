using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public interface IBaseService
    {
        void CountClick(string userHostAddress, string url, string keyView);
    }
}
