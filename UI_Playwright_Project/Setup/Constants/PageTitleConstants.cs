using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Playwright_Project.Setup.Constants
{
    public class PageTitleConstants
    {
        private static readonly string _homePageTitle = "XYZ FASHION STORE - Online prodavnica";
        private static readonly string _cartPageTitle = "Korpa za kupovinu";
        public static string HomePageTitle => _homePageTitle;
        public static string CartPageTitle => _cartPageTitle;
    }
}
