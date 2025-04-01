using Allure.Net.Commons;
using Reqnroll;
using UI_Playwright_Project.Setup;

namespace UI_Playwright_Project
{
    [Binding]
    public class BaseSteps
    {

        [Before]
        public void FirstBeforeScenario()
        {
        }

        [AfterScenario]
        public void AfterScenario()
        {
        }
    }
}
