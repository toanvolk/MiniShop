namespace MiniShop.Identity
{

    public class DataResponeDto
    {
        public StatuCodeEnum Statu { get; set; }
        public string Message { get; set; }
    }
    public class DataResponeCommon<T> where T : class
    {
        public StatuCodeEnum Statu { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Total { get; set; }
    }
}
