using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure
{
    public static class ListExtension
    {
        public static void SetIndex<T>(this List<T> indexDto, int page, int pageSize)
        {
            if(indexDto.Count > 0) 
                if (indexDto[0] is IndexDto)
                {                    
                    int i = 0;
                    indexDto.ForEach((o) => { i++; (o as IndexDto).Index = (page - 1) * pageSize + i; });
                }
        }
        public static void SetIndex<T>(this List<T> indexDto)
        {
            if (indexDto.Count > 0)
                if (indexDto[0] is IndexDto)
                {
                    int i = 0;
                    indexDto.ForEach((o) => { i++; (o as IndexDto).Index = i; });
                }
        }
    }
}
