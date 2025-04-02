using Reqnroll.BoDi;
using Microsoft.Playwright;
using Reqnroll;
using Shouldly;
using UI_Playwright_Project.Pages;
using UI_Playwright_Project.Setup.Constants;
using UI_Playwright_Project.Setup.Enums;

namespace UI_Playwright_Project
{
    [Binding, Scope(Tag = "UI")]
    public class StepDefinitions : BasePage
    {
        private readonly IObjectContainer _objectContainer;
        private readonly IPage _page;
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePage _homePage;
        private readonly ProductDetailsPage _productDetailsPage;
        private readonly CartPage _cartPage;
        private readonly ShippingPage _shippingPage;
        private readonly PaymentPage _paymentPage;
        public StepDefinitions(
            IObjectContainer objectContainer,
            IPage page,
            ScenarioContext scenarioContext,
            HomePage homePage,
            ProductDetailsPage productDetailsPage,
            CartPage cartPage,
            ShippingPage shippingPage,
            PaymentPage paymentPage
            ) : base(page)
        {
            _objectContainer = objectContainer;
            _page = page;
            _scenarioContext = scenarioContext;
            _homePage = homePage;
            _productDetailsPage = productDetailsPage;
            _cartPage = cartPage;
            _shippingPage = shippingPage;
            _paymentPage = paymentPage;
        }
        [Given("User opens Home Page")]
        public async Task GivenUserOpensHomePage()
        {
            await _homePage.GoToPage(UrlConstants.BaseUrl);
            await _homePage.ClickOnAsync(_homePage.AcceptCookies);
            await Expect(_page).ToHaveTitleAsync(PageTitleConstants.HomePageTitle);
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
            await _shippingPage.GoToPage(UrlConstants.ShippingPageUrl);
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
