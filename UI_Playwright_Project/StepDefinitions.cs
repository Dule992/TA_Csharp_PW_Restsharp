using Microsoft.Playwright;
using NUnit.Allure.Attributes;
using Reqnroll;
using Shouldly;
using UI_Playwright_Project.Pages;
using UI_Playwright_Project.Setup;
using UI_Playwright_Project.Setup.Constants;
using UI_Playwright_Project.Setup.Enums;

namespace UI_Playwright_Project
{
    [Binding]
    public class StepDefinitions : PageTest
    {
        private IPage _page;
        private ScenarioContext _scenarioContext;
        private HomePage _homePage;
        private ProductDetailsPage _productDetailsPage;
        private CartPage _cartPage;
        private ShippingPage _shippingPage;
        private PaymentPage _paymentPage;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        [AllureLabel("Setup", "web")]
        public async Task TestIntilizeCheckoutStepDefinitions()
        {
            await PlaywrightProvider.InitPlaywright();
            await PlaywrightProvider.CreatePageAsync();
            _page = PlaywrightProvider.Page;
            _homePage = new HomePage(_page);
            _productDetailsPage = new ProductDetailsPage(_page);
            _cartPage = new CartPage(_page);
            _shippingPage = new ShippingPage(_page);
            _paymentPage = new PaymentPage(_page);
        }

        [Given("User opens Home Page")]
        public async Task GivenUserOpensHomePage()
        {
            await _homePage.GoToPage(UrlConstants.BaseUrl);
            await _homePage.ClickOnAsync(_homePage.AcceptCookies);
            await Expect(PlaywrightProvider.Page).ToHaveTitleAsync(PageTitleConstants.HomePageTitle);
        }

        [Given("User login to the account")]
        public async Task GivenUserLoginToTheAccount()
        {
            await _homePage.ClickOnAsync(_homePage.LoginMenuButton);
            await _homePage.ClickOnAsync(_homePage.SignInButton);
            await _homePage.LoginAsync(CredentialsConstants.Email, CredentialsConstants.Password);
            await _cartPage.GoToPage(UrlConstants.CartPageUrl);
            bool isCarEmpty = await _cartPage.EmptyCartButton.IsVisibleAsync();

            if (isCarEmpty)
            {
                await _cartPage.ClickOnAsync(_cartPage.EmptyCartButton);
            }
        }

        [When("User selects a product item")]
        public async Task WhenUserSelectsAProductItem()
        {
            await _cartPage.GoToPage(UrlConstants.BaseUrl);
            await _homePage.ClickOnAsync(_homePage.ProductItem(ProductConstants.ProductItemName));
            await Expect(_page).ToHaveURLAsync(new Regex(pattern: $".*{UrlConstants.ProductItemUrl}"));
        }

        [When("User clicks on cart button")]
        public async Task WhenUserClicksOnCartButton()
        {
            await Expect(_productDetailsPage.ProductColor).ToContainTextAsync(ProductConstants.ProductItemColor);
            _scenarioContext.Add("ProductPrice", await _productDetailsPage.ProductPrice.TextContentAsync());
            await _productDetailsPage.ClickOnAsync(_productDetailsPage.AddToCartButton);
            await Expect(_productDetailsPage.SuccessMessage).ToContainTextAsync(ProductConstants.ProductItemName,
                new LocatorAssertionsToContainTextOptions { Timeout = (int)Timeouts.ElementsLoadInMS });
            await Expect(_productDetailsPage.CartCounterNumber).ToContainTextAsync("1");
        }

        [Then("User should see the product item in the cart details")]
        public async Task ThenUserShouldSeeCartDetails()
        {
            await _cartPage.GoToPage(UrlConstants.CartPageUrl);
            await Expect(_page).ToHaveTitleAsync(PageTitleConstants.CartPageTitle);
            await Expect(_cartPage.ProductColumn).ToContainTextAsync(ProductConstants.ProductItemName);
            await Expect(_cartPage.ProductColumn).ToContainTextAsync(ProductConstants.ProductItemColor);
            await Expect(_cartPage.PriceColumn).ToContainTextAsync(_scenarioContext.Get<string>("ProductPrice"));
            var quantity = await _cartPage.QuantityColumn.InputValueAsync();
            quantity.ShouldBeEquivalentTo("1");
            await Expect(_cartPage.TotalColumn).ToContainTextAsync(_scenarioContext.Get<string>("ProductPrice"));
        }

        [When("User clicks on proceed payment button")]
        public async Task WhenUserClicksOnProccedPaymentButton()
        {
            await _cartPage.ClickOnAsync(_cartPage.ProceedToPaymentButton);
        }

        [Then("User should see the shipping form page opened")]
        public async Task ThenUserShouldSeeTheShippingFormPageOpened()
        {
            Lazy<Task> shippingPage = new Lazy<Task>(() => _shippingPage.GoToPage(UrlConstants.ShippingPageUrl));
            shippingPage.Value.Wait();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Expect(_shippingPage.ShippingForm).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions
            { Timeout = (int)Timeouts.ElementsLoadInMS });
        }

        [When("User fills the shipping form")]
        public async Task WhenUserFillsTheShippingForm()
        {
            await _shippingPage.FillShippingDetailsAsync();
            await _shippingPage.ContinueButton.ScrollIntoViewIfNeededAsync();
            await _shippingPage.ClickOnAsync(_shippingPage.ContinueButton);
        }

        [Then("User should see the payment page opened")]
        public async Task ThenUserShouldSeeThePaymentPageOpened()
        {
            await Expect(_page).ToHaveURLAsync(new Regex(pattern: $".*{UrlConstants.PaymentPageUrl}"),
                new PageAssertionsToHaveURLOptions { Timeout = (int)Timeouts.NavigationTimeoutInMS });
        }
    }
}
