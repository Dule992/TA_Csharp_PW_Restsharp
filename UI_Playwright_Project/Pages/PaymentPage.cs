using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class PaymentPage: BasePage
    {
        readonly IPage _page;

        public PaymentPage(IPage page) : base(page)
        {
            _page = page;
        }

        // Selectors
        private string _addToCartButton = "#product-addtocart-button";

        // Locators
        public ILocator AddToCartButton => _page.Locator(_addToCartButton);

        // Methods
    }
}
