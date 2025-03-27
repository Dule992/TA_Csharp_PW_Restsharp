using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Constants;

namespace UI_Playwright_Project.Pages
{
    public class ShippingPage: BasePage
    {
        private new readonly IPage _page;

        public ShippingPage(IPage page) : base(page)
        {
            _page = page;
        }

        public ILocator ShippingTitle => _page.GetByText(ShippingDataConstants.ShippingTitle);
        public ILocator FirstNameField => _page.Locator("input[name='firstname']");
        public ILocator LastNameField => _page.Locator("input[name='lastname']");
        public ILocator CompanyNameField => _page.Locator("input[name='company']");
        public ILocator AddressField => _page.Locator("input[name='street[0]']");
        public ILocator CountryField => _page.Locator("select[name='country_id']");
        public ILocator CityField => _page.Locator("input[name='city']");
        public ILocator PostcodeField => _page.Locator("input[name='postcode']");
        public ILocator PhoneNumberField => _page.Locator("input[name='telephone']");
        public ILocator ContinueButton => _page.GetByText(ShippingDataConstants.ContinueButton);

        public async Task FillShippingDetailsAsync(string firstName, string lastName, string companyName, 
            string address, string country, string city, string postcode, string phoneNumber)
        {
            await FirstNameField.FillAsync(firstName);
            await LastNameField.FillAsync(lastName);
            await CompanyNameField.FillAsync(companyName);
            await AddressField.FillAsync(address);
            await CountryField.SelectOptionAsync(country);
            await CityField.FillAsync(city);
            await PostcodeField.FillAsync(postcode);
            await PhoneNumberField.FillAsync(phoneNumber);
            await ClickOnAsync(ContinueButton);
        }
    }
}
