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

        // Selectors
        private string _addToCartButton = "#product-addtocart-button";
        private string _cartCounterNumber = ".counter-number";
        private string _productOptions = ".product-detail-infomation-content";
        private string _productPrice = "span.price";
        private string _productColor = ".swatch-attribute.color";
        private string _successMessage = ".message-success";

        // Locators
        public ILocator AddToCartButton => _page.Locator(_addToCartButton);
        public ILocator CartCounterNumber => _page.Locator(_cartCounterNumber);
        public ILocator ProductOptions => _page.Locator(_productOptions);
        public ILocator ProductPrice => ProductOptions.Locator(_productPrice);
        public ILocator ProductColor => ProductOptions.Locator(_productColor);
        public ILocator SuccessMessage => _page.Locator(_successMessage);

        // Methodss
    }
}
