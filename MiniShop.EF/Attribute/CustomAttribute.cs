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
    public class CollectionCustomAttribute : Attribute
    {
        public bool AllowModify { get; set; }
        public CollectionCustomAttribute(bool modify = false)
        {
            AllowModify = modify;
        }
    }
}
