using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Constants;

namespace UI_Playwright_Project.Pages
{
    public class CartPage : BasePage
    {
        private new readonly IPage _page;

        public CartPage(IPage page) : base(page)
        {
            _page = page;
        }

        public ILocator ProductColumn => _page.Locator("[data-th='Proizvod']");
        public ILocator PriceColumn => _page.Locator("[data-th='Cena']");
        public ILocator QuantityColumn => _page.GetByTitle("Količina");
        public ILocator TotalColumn => _page.Locator("[data-th='Ukupno'] .cart-price");
        public ILocator ShippingPrice => _page.Locator("#cart-totals [data-th='Dostava']");
        public ILocator CartTotalAmount => _page.Locator("#cart-totals strong .price");
        public ILocator ProceedToPaymentButton => _page.Locator(".checkout-methods-items button.action.primary.checkout");
        public ILocator EmptyCartButton => _page.Locator("#empty_cart_button");
        public ILocator MiniCartIsNotEmptyButton => _page.Locator(".minicart-not-empty");

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
