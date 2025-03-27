namespace UI_Playwright_Project.Setup.Constants
{
    public class ShippingDataConstants
    {
        private static readonly string _shippingTitle = "Plaćanje";
        // Shipping Details
        private static readonly string _firstName = "Tester";
        private static readonly string _lastName = "Testerovic";
        private static readonly string _companyName = "XYZ Fashion Store";
        private static readonly string _address = "Bezimena 11";
        private static readonly string _country = "Srbija";
        private static readonly string _city = "Beograd";
        private static readonly string _postcode = "11000";
        private static readonly string _phoneNumber = "+381 62 123 4567";
        private static readonly string _continueButton = "Sledeće";

        public static string ShippingTitle => _shippingTitle;
        public static string FirstName => _firstName;
        public static string LastName => _lastName;
        public static string CompanyName => _companyName;
        public static string Address => _address;
        public static string Country => _country;
        public static string City => _city;
        public static string Postcode => _postcode;
        public static string PhoneNumber => _phoneNumber;
        public static string ContinueButton => _continueButton;

    }
}
