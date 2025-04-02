using Microsoft.Playwright;
using NLog;
using UI_Playwright_Project.Setup.Enums;
using BrowserType = UI_Playwright_Project.Setup.Enums.BrowserType;

namespace UI_Playwright_Project.Setup
{
    public class PlaywrightProvider
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static IBrowser _browser;
        private static IBrowserContext _browserContext;
        private static IPage _page;
        public static IBrowser Browser => _browser;
        public static IBrowserContext BrowserContext => _browserContext;
        public static IPage Page => _page;

        public static async Task InitPlaywright()
        {
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), ConfigReader.GetBrowserType());

            var playwright = await Playwright.CreateAsync().ConfigureAwait(false);
            _browser = await CreateBrowserInstance(playwright, browserType).ConfigureAwait(false);
            _browserContext = await CreateBrowserContext(playwright, _browser).ConfigureAwait(false);
            await _browserContext.ClearCookiesAsync();

            Log.Debug($"{Browser.BrowserType.Name.ToUpper()} version {Browser.Version.ToUpper()} is launched.");
        }

        /// <param name="playwright">Playwright object instance</param>
        /// <param name="browserType">An option from BrowserType enum {CHROMIUM, CHROME, MSEDGE, FIREFOX, SAFARI}</param>
        private static async Task<IBrowser> CreateBrowserInstance(IPlaywright playwright, BrowserType browserType)
        {
            var options = CreateBrowserOptions();
            return browserType switch
            {
                BrowserType.CHROMIUM => await playwright.Chromium.LaunchAsync(options).ConfigureAwait(false),
                BrowserType.CHROME => await playwright.Chromium.LaunchAsync(CreateBrowserOptions(BrowserType.CHROME.ToString().ToLower())).ConfigureAwait(false),
                BrowserType.MSEDGE => await playwright.Chromium.LaunchAsync(CreateBrowserOptions(BrowserType.MSEDGE.ToString().ToLower())).ConfigureAwait(false),
                BrowserType.FIREFOX => await playwright.Firefox.LaunchAsync(options).ConfigureAwait(false),
                BrowserType.SAFARI => await playwright.Webkit.LaunchAsync(options),
                _ => throw new ApplicationException("Unsupported BrowserType was provided: " + browserType),
            };
        }

        /// <summary>
        /// Setup browser's Launch options
        /// </summary>
        private static BrowserTypeLaunchOptions CreateBrowserOptions(string channel = null)
        {
            return new BrowserTypeLaunchOptions
            {
                DownloadsPath = Directory.GetCurrentDirectory(),
                Timeout = (float)Timeouts.BrowserStartTimeoutInMS,
                Headless = ConfigReader.GetHeadlessMode(),
                Channel = channel
            };
        }

        /// <summary>
        /// Creates a browser context
        /// </summary>
        /// <param name="playwright">A playwright instance</param>
        /// <param name="browser">A browser instance</param>
        private static async Task<IBrowserContext> CreateBrowserContext(IPlaywright playwright, IBrowser browser)
        {
            var contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ConfigReader.GetDefaultViewportSize(),
                IgnoreHTTPSErrors = true,
            };

            var deviceName = ConfigReader.GetPlaywrightDeviceName();

            if (!string.IsNullOrWhiteSpace(deviceName))
            {
                contextOptions = playwright.Devices[deviceName];
            }

            var context = await browser.NewContextAsync(contextOptions).ConfigureAwait(false);

            context.SetDefaultNavigationTimeout((float)Timeouts.NavigationTimeoutInMS);
            context.SetDefaultTimeout((float)Timeouts.ActionsTimeoutInMS);

            return context;
        }

        public static async Task CreatePageAsync()
        {
            _page = await Browser.NewPageAsync();
        }

        public static async Task CloseBrowserAsync()
        {
            foreach (var context in Browser.Contexts)
            {
                await context.CloseAsync().ConfigureAwait(false);
            }

            await Browser.CloseAsync().ConfigureAwait(false);
        }
    }
}
