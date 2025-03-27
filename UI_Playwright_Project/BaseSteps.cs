using NLog;
using Reqnroll;
using UI_Playwright_Project.Setup;

namespace UI_Playwright_Project
{
    [Binding]
    public class BaseSteps
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [Before]
        public void FirstBeforeScenario()
        {
            Log.Info("Test Exectuion Started !");
         
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Log.Info("Test Execution Completed !");
        }
    }
}
