﻿using MiniShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class CategoryDto : IndexDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public bool NotUse { get; set; }
        public int? SortIndex { get; set; }
        public ICollection<ProductDto> Pruducts { get; set; }
    }
}
