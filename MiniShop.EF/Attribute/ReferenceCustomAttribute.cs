using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.EF
{
    public class ReferenceCustomAttribute : Attribute
    {
        public bool AllowModify { get; set; }
        public ReferenceCustomAttribute(bool modify = false)
        {
            AllowModify = modify;
        }
    }
}
