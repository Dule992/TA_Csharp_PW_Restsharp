using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Constants;

namespace UI_Playwright_Project.Pages
{
    public class CartPage: BasePage
    {
        private new readonly IPage _page;

        public CartPage(IPage page) : base(page)
        {
            _page = page;
        }

        public ILocator ProductColumn => _page.Locator("[data-th='Proizvod']");
        public ILocator PriceColumn => _page.Locator("[data-th='Cena']");
        public ILocator QuantityColumn => _page.Locator("[data-th='Količina']");
        public ILocator TotalColumn => _page.Locator("[data-th='Ukupno']");
        public ILocator ProceedToPaymentButton => _page.GetByTitle(ProductConstants.ProceedToPaymentButton);
        public ILocator EmailInputField => _page.Locator("#customer-email");
        public ILocator PasswordInputField => _page.Locator("#pass");
        public ILocator SubmitLoginButton => _page.Locator("#send2");

        public async Task LoginAsync(string email, string password)
        {
            await EmailInputField.FillAsync(email);
            await PasswordInputField.FillAsync(password);
            await ClickOnAsync(SubmitLoginButton);
        }

    }
}
