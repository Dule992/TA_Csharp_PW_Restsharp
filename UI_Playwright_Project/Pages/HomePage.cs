using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class HomePage: BasePage
    {
        private new readonly IPage _page;

        public HomePage(IPage page): base(page)
        {
            _page = page;
        }

        public ILocator ProductItem(string title) => _page.GetByTitle(title).First;
        
    }
}
