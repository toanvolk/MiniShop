﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.App
{
    public interface IHomeService
    {
        ICollection<ProductReviewDto> GetProductReview(DateTime fromDate, DateTime toDate);
    }
}
