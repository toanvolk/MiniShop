using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class PageFilterDto<T>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string TextSearch { get; set; }

        public T FilterData
        {
            get
            {
                if (string.IsNullOrEmpty(TextSearch)) return default(T);
                return JsonConvert.DeserializeObject<T>(TextSearch);
            }
        }

        public int SkipCount
        {
            get
            {
                return ((PageSize ?? 10) - 1) * (PageNumber ?? 1);
            }
        }
        public int TakeRecords
        {
            get
            {
                return PageSize ?? 10;
            }
        }
    }
    public class PageFilterDto
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    
        public int SkipCount
        {
            get
            {
                return ((PageNumber ?? 1) - 1) * (PageSize ?? 10);
            }
        }
        public int TakeRecords
        {
            get
            {
                return PageSize ?? 10;
            }
        }
    }
}
