using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Enums;

namespace UI_Playwright_Project.Pages
{
    public class ProductDetailsPage : BasePage
    {
        private new readonly IPage _page;

        public ProductDetailsPage(IPage page) : base(page)
        {
            _page = page;
        }

        public ILocator AddToCartButton => _page.Locator("#product-addtocart-button");
        public ILocator CartCounterNumber => _page.Locator(".counter-number");
        public ILocator ProductOptions => _page.Locator(".product-detail-infomation-content");
        public ILocator ProductPrice => ProductOptions.Locator("span.price");
        public ILocator ProductColor => ProductOptions.Locator(".swatch-attribute.color");
        public ILocator SuccessMessage => _page.Locator(".message-success");
    }
}
