using Microsoft.Playwright;

namespace UI_Playwright_Project.Pages
{
    public class BasePage: PageTest
    {
        public readonly IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
        }
        public ILocator EmailInputField => _page.Locator("#email");
        public ILocator PasswordInputField => _page.GetByTitle("Lozinka");
        public ILocator SubmitLoginButton => _page.Locator(".action.login.btn.btn-default");

        public async Task LoginAsync(string email, string password)
        {
            await EmailInputField.FillAsync(email, new LocatorFillOptions { Timeout = 1000 });
            await PasswordInputField.FillAsync(password, new LocatorFillOptions { Timeout = 1000 });
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

        public async Task RetryAsync(Func<Task> action, int maxAttempts = 3, int delayMs = 1000)
        {
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    await action();
                    return; // Success, exit loop
                }
                catch (Exception ex) when (attempt < maxAttempts)
                {
                    Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                    await Task.Delay(delayMs * attempt); // Exponential backoff
                }
            }
            throw new Exception($"Action failed after {maxAttempts} attempts");
        }

    }
}
