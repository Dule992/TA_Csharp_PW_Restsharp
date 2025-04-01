using Bogus;
using Microsoft.Playwright;
using UI_Playwright_Project.Setup.Constants;

namespace UI_Playwright_Project.Pages
{
    public class ShippingPage : BasePage
    {
        private new readonly IPage _page;
        private Faker _faker = new Faker();

        public ShippingPage(IPage page) : base(page)
        {
            _page = page;
        }

        // Selectors
        string _shippingForm = "#shipping";
        string _firstNameField = "input[name='firstname']";
        string _lastNameField = "input[name='lastname']";
        string _companyNameField = "input[name='company']";
        string _addressField = "input[name='street[0]']";
        string _countryField = "select[name='country_id']";
        string _cityField = "input[name='city']";
        string _postcodeField = "input[name='postcode']";
        string _phoneNumberField = "input[name='telephone']";
        string _continueButton = ShippingDataConstants.ContinueButton;
        string _fieldsError = ".field-error";

        // Locators
        public ILocator ShippingForm => _page.Locator(_shippingForm);
        public ILocator FirstNameField => _page.Locator(_firstNameField);
        public ILocator LastNameField => _page.Locator(_lastNameField);
        public ILocator CompanyNameField => _page.Locator(_companyNameField);
        public ILocator AddressField => _page.Locator(_addressField);
        public ILocator CountryField => _page.Locator(_countryField);
        public ILocator CityField => _page.Locator(_cityField);
        public ILocator PostcodeField => _page.Locator(_postcodeField);
        public ILocator PhoneNumberField => _page.Locator(_phoneNumberField);
        public ILocator ContinueButton => _page.GetByText(_continueButton);
        public ILocator FieldsError => _page.Locator(_fieldsError);

        // Methods
        public async Task FillShippingDetailsAsync()
        {
            for (int i = 0; i < 5; i++)
            {
                string firstName = _faker.Name.FirstName();
                string lastName = _faker.Name.LastName();
                string companyName = _faker.Company.CompanyName();
                string address = ShippingDataConstants.Address;
                string city = ShippingDataConstants.City;
                string postcode = ShippingDataConstants.Postcode;
                string phoneNumber = ShippingDataConstants.PhoneNumber;

                await FirstNameField.ClearAsync();
                await _page.Keyboard.TypeAsync(firstName, new KeyboardTypeOptions { Delay = 100 });
                await Expect(FirstNameField).ToHaveValueAsync(firstName);
                //await FirstNameField.FillAsync(firstName, new LocatorFillOptions { Timeout = 1000 });
                await LastNameField.ClearAsync();
                await _page.Keyboard.TypeAsync(lastName, new KeyboardTypeOptions { Delay = 100 });
                await Expect(LastNameField).ToHaveValueAsync(lastName);
                //await LastNameField.FillAsync(lastName, new LocatorFillOptions { Timeout = 1000 });
                await CompanyNameField.ClearAsync();
                await _page.Keyboard.TypeAsync(companyName, new KeyboardTypeOptions { Delay = 100 });
                await Expect(CompanyNameField).ToHaveValueAsync(companyName);
                //await CompanyNameField.FillAsync(companyName, new LocatorFillOptions { Timeout = 1000 });
                await AddressField.ClearAsync();
                await _page.Keyboard.TypeAsync(address, new KeyboardTypeOptions { Delay = 100 });
                await Expect(AddressField).ToHaveValueAsync(address);
                //await AddressField.FillAsync(address, new LocatorFillOptions { Timeout = 1000 });
                await CityField.ClearAsync();
                await _page.Keyboard.TypeAsync(city, new KeyboardTypeOptions { Delay = 100 });
                await Expect(CityField).ToHaveValueAsync(city);
                //await CityField.FillAsync(city, new LocatorFillOptions { Timeout = 1000 });
                await PostcodeField.ClearAsync();
                await _page.Keyboard.TypeAsync(postcode, new KeyboardTypeOptions { Delay = 100 });
                await Expect(PostcodeField).ToHaveValueAsync(postcode);
                //await PostcodeField.FillAsync(postcode, new LocatorFillOptions { Timeout = 1000 });
                await PhoneNumberField.ClearAsync();
                await _page.Keyboard.TypeAsync(phoneNumber, new KeyboardTypeOptions { Delay = 100 });
                //await PhoneNumberField.FillAsync(phoneNumber, new LocatorFillOptions { Timeout = 1000 });

                bool isFieldErrorExist = await FieldsError.CountAsync() > 0;
                if (isFieldErrorExist.Equals(true))
                {
                    Console.WriteLine("Field error exists. Retrying...");
                    continue;
                }
                else
                {
                    break;
                };
            }
        }
    }
}
