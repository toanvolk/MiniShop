using System;
using System.Collections.Generic;

namespace MiniShop.App
{
    public interface IHomeService
    {
        ICollection<ProductReviewDto> GetProductReview(DateTime fromDate, DateTime toDate);
        CounterDto GetCounter();
        void Counting(string key);
    }
}
