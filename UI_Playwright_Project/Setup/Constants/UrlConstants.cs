using System;
namespace UI_Playwright_Project.Setup.Constants
{
    public class UrlConstants
    {
        private static readonly string _baseUrl = "https://xyzfashionstore.com/rs/";
        private static readonly string _cartPageUrl = _baseUrl + "checkout/cart/";
        private static readonly string _productItemUrl = ".*armani-exchange-crna-zenska-torbica";
        private static readonly string _shippingPageUrl = _baseUrl + "checkout/#shipping";

        public static string BaseUrl => _baseUrl;
        public static string CartPageUrl => _cartPageUrl;
        public static string ProductItemUrl => _productItemUrl;
        public static string ShippingPageUrl => _shippingPageUrl;
    }
}
