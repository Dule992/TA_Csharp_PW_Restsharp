using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class PaymentPage: BasePage
    {
        private new readonly IPage _page;

        public PaymentPage(IPage page) : base(page)
        {
            _page = page;
        }

        public ILocator AddToCartButton => _page.Locator("#product-addtocart-button");
    }
}
