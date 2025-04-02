using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class HomePage : BasePage
    {
        readonly IPage _page;

        public HomePage(IPage page) : base(page)
        {
            _page = page;
        }

        // Selectors
        private string _loginMenuButton = ".dropdown-toggle.guest";
        private string _signInButton = ".link.authorization-link";

        // Locators
        public ILocator LoginMenuButton => _page.Locator(_loginMenuButton);
        public ILocator SignInButton => _page.Locator(_signInButton);
        public ILocator ProductItem(string title) => _page.GetByTitle(title).First;

        // Methods
    }
}
