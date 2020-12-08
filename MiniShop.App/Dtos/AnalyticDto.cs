using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.App
{
    public class AnalyticDto
    {

    }
    public class ClickView
    {
        public Guid TouchId { get; set; }
        public string Url { get; set; }
        public int ClickCount { get; set; }
    }
    public class ClickViewDetail
    {
        public string AddressId { get; set; }
        public DateTime ClickDate { get; set; }
    }
}
