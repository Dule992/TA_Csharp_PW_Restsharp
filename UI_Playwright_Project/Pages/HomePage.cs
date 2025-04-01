using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class HomePage : BasePage
    {
        private new readonly IPage _page;

        public HomePage(IPage page) : base(page)
        {
            _page = page;
        }

        public ILocator LoginMenuButton => _page.Locator(".dropdown-toggle.guest");
        public ILocator SignInButton => _page.Locator(".link.authorization-link");
        public ILocator ProductItem(string title) => _page.GetByTitle(title).First;

    }
}
