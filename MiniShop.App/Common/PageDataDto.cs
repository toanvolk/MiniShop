using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class PageDataDto<T>
    {
        public IList<T> Source { get; set; }
        public int Total { get; set; }

        public PageDataDto(IList<T> source, int total)
        {
            Source = source;
            Total = total;
        }
    }
}
