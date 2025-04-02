using Reqnroll.BoDi;
using Microsoft.Playwright;
using Reqnroll;
using UI_Playwright_Project.Setup;
using UI_Playwright_Project.Pages;

namespace UI_Playwright_Project
{
    [Binding]
    public class BaseSteps
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        public readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private IPage _page;
        private IBrowserContext _browserContext;
        private IBrowser _browser;

        public BaseSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext) 
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 2), Scope(Tag = "UI")]
        public async Task TestIntilizeCheckoutStepDefinitions()
        {
            await PlaywrightProvider.InitPlaywright();
            _browser = PlaywrightProvider.Browser;
            _browserContext = PlaywrightProvider.BrowserContext;
            _page = await PlaywrightProvider.BrowserContext.NewPageAsync();

            _objectContainer.RegisterInstanceAs(_browser);
            _objectContainer.RegisterInstanceAs(_browserContext);
            _objectContainer.RegisterInstanceAs(_page);
        }

        [BeforeScenario(Order = 3)]
        public async Task StartTracing()
        {
            await _browserContext.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            IBrowser browser = _objectContainer.Resolve<IBrowser>();

            if (browser.IsConnected)
            {
                await PlaywrightProvider.CloseBrowserAsync();
                Log.Debug($"{browser.BrowserType.Name.ToUpper()} browser is closed successfully.");
            }
        }
    }
}
