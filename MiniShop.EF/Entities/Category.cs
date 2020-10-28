using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniShop.EF
{
    public class Category : AuditableEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
