using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class BasePage
    {
        public readonly IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
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
