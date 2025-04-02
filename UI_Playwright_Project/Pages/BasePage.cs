using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Enums;

namespace UI_Playwright_Project.Pages
{
    public class BasePage: PageTest
    {
        readonly IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
        }

        // Selectors
        private string _emailInputField = "#email";
        private string _passwordInputField = "Lozinka";
        private string _submitLoginButton = ".action.login.btn.btn-default";

        // Locators
        public ILocator EmailInputField => _page.Locator(_emailInputField);
        public ILocator PasswordInputField => _page.GetByTitle(_passwordInputField);
        public ILocator SubmitLoginButton => _page.Locator(_submitLoginButton);

        // Methods
        public async Task LoginAsync(string email, string password)
        {
            await EmailInputField.FillAsync(email, new LocatorFillOptions { Timeout = (int)Timeouts.ElementsLoadInMS });
            await PasswordInputField.FillAsync(password, new LocatorFillOptions { Timeout = (int)Timeouts.ElementsLoadInMS });
            await ClickOnAsync(SubmitLoginButton);
        }

        public ILocator AcceptCookies => _page.GetByText("Prihvati sve");

        public async Task GoToPage(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task ClickOnAsync(ILocator locator)
        {
            await locator.ClickAsync();
        }
    }
}
