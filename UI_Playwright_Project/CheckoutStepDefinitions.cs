using Microsoft.Playwright;
using Reqnroll;
using UI_Playwright_Project.Pages;
using UI_Playwright_Project.Setup;
using UI_Playwright_Project.Setup.Constants;

namespace UI_Playwright_Project
{
    [Binding]
    public class CheckoutStepDefinitions : PageTest
    {
        private IPage _page;
        private ScenarioContext _scenarioContext;
        private HomePage _homePage;
        private ProductDetailsPage _productDetailsPage;
        private CartPage _cartPage;
        private ShippingPage _shippingPage;
        private PaymentPage _paymentPage;

        public CheckoutStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
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

        [When("User selects a product item")]
        public async Task WhenUserSelectsAProductItem()
        {
            await _homePage.ClickOnAsync(_homePage.ProductItem(ProductConstants.ProductItemName));
            await Expect(_page).ToHaveURLAsync(new Regex(pattern: $".*{UrlConstants.ProductItemUrl}"));
        }

        [When("User clicks on cart button")]
        public async Task WhenUserClicksOnCartButton()
        {
            await Expect(_productDetailsPage.ProductColor).ToContainTextAsync(ProductConstants.ProductItemColor);
            _scenarioContext.Add("ProductPrice", await _productDetailsPage.ProductPrice.TextContentAsync());
            await _productDetailsPage.ClickOnAsync(_productDetailsPage.AddToCartButton);
            await Expect(_productDetailsPage.SuccessMessage).ToContainTextAsync(ProductConstants.ProductItemName);
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
            await Expect(_cartPage.QuantityColumn).ToContainTextAsync("1");
            await Expect(_cartPage.TotalColumn).ToContainTextAsync(_scenarioContext.Get<string>("ProductPrice"));
        }

        [When("User clicks on proceed payment button")]
        public async Task WhenUserClicksOnProccedPaymentButton()
        {
            await _cartPage.ClickOnAsync(_cartPage.ProceedToPaymentButton);
            await _cartPage.LoginAsync(CredentialsConstants.Email, CredentialsConstants.Password);
        }

        [Then("User should see the shipping form page opened")]
        public async Task ThenUserShouldSeeTheShippingFormPageOpened()
        {
           await Expect(_page).ToHaveURLAsync(new Regex(pattern: $".*{UrlConstants.ShippingPageUrl}"));
           await Expect(_shippingPage.ShippingTitle).ToBeVisibleAsync();
        }

        [When("User fills the shipping form")]
        public async Task WhenUserFillsTheShippingForm()
        {
           await _shippingPage.FillShippingDetailsAsync(
               ShippingDataConstants.FirstName, 
               ShippingDataConstants.LastName,
               ShippingDataConstants.CompanyName, 
               ShippingDataConstants.Address, 
               ShippingDataConstants.Country,
               ShippingDataConstants.City, 
               ShippingDataConstants.Postcode, 
               ShippingDataConstants.PhoneNumber);
        }

    }
}
