namespace MiniShop.App
{
    public enum ProductSortEnum
    {
        COMMON,
        INCREASE_PRICE, 
        DECREASE_PRICE
    }   
    public static class ProductSortDefine
    {
        public static string GetString(this ProductSortEnum productSortEnum)
        {
            string str;
            switch (productSortEnum)
            {
                case ProductSortEnum.COMMON: str = "Phổ biến"; break;
                case ProductSortEnum.INCREASE_PRICE: str = "Giá tăng dần"; break;
                case ProductSortEnum.DECREASE_PRICE: str = "Giá giảm dần"; break;
                default: str = ""; break;
            }

            return str;
        }
    }
}
