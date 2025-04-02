using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Constants;

namespace UI_Playwright_Project.Pages
{
    public class CartPage : BasePage
    {
        readonly IPage _page;

        public CartPage(IPage page) : base(page)
        {
            _page = page;
        }

        // Selectors
        private string _emptyCartButton = "#empty_cart_button";
        private string _miniCartIsNotEmptyButton = ".minicart-not-empty";
        private string _productColumn = "[data-th='Proizvod']";
        private string _priceColumn = "[data-th='Cena']";
        private string _quantityColumn = "Količina";
        private string _totalColumn = "[data-th='Ukupno'] .cart-price";
        private string _shippingPrice = "#cart-totals [data-th='Dostava']";
        private string _cartTotalAmount = "#cart-totals strong .price";
        private string _proceedToPaymentButton = ".checkout-methods-items button.action.primary.checkout";

        // Locators
        public ILocator ProductColumn => _page.Locator(_productColumn);
        public ILocator PriceColumn => _page.Locator(_priceColumn);
        public ILocator QuantityColumn => _page.GetByTitle(_quantityColumn);
        public ILocator TotalColumn => _page.Locator(_totalColumn);
        public ILocator ShippingPrice => _page.Locator(_shippingPrice);
        public ILocator CartTotalAmount => _page.Locator(_cartTotalAmount);
        public ILocator ProceedToPaymentButton => _page.Locator(_proceedToPaymentButton);
        public ILocator EmptyCartButton => _page.Locator(_emptyCartButton);
        public ILocator MiniCartIsNotEmptyButton => _page.Locator(_miniCartIsNotEmptyButton);

        // Methods
        public async Task<int> GetTotalAmountAsync(string productPrice)
        {
            var productValue = Regex.Replace(productPrice, @"[^\d]", "");
            var shippingPrice = await ShippingPrice.TextContentAsync();
            var shippingValue = Regex.Replace(shippingPrice, @"[^\d]", "");
            var totalAmount = int.Parse(productValue) + int.Parse(shippingValue);
            return totalAmount;
        }
    }
}
