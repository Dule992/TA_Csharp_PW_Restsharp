using Microsoft.Playwright;
using System.Globalization;

namespace UI_Playwright_Project.Setup
{
    public static class ConfigReader
    {
        public static string GetBrowserType()
        {
            return Environment.GetEnvironmentVariable("BROWSER_TYPE")?.Trim().ToUpper(new CultureInfo("en-US", false)) ?? "CHROMIUM";
        }

        public static void SetBrowserType(string value)
        {
            Environment.SetEnvironmentVariable("BROWSER_TYPE", value.Trim().ToUpper(new CultureInfo("en-US", false)));
        }

        public static bool GetHeadlessMode()
        {
            return Convert.ToBoolean(Environment.GetEnvironmentVariable("HEADLESS_MODE"));
        }

        public static void SetHeadlessMode(bool value)
        {
            Environment.SetEnvironmentVariable("HEADLESS_MODE", value.ToString());
        }

        public static bool GetExtentReportMode()
        {
            return Environment.GetEnvironmentVariable("EXTENT_REPORT_MODE") is null ||
                   Convert.ToBoolean(Environment.GetEnvironmentVariable("EXTENT_REPORT_MODE"));
        }

        public static void SetExtentReportMode(bool value)
        {
            Environment.SetEnvironmentVariable("EXTENT_REPORT_MODE", value.ToString());
        }

        public static string GetExtentReportName()
        {
            return Environment.GetEnvironmentVariable("EXTENT_REPORT_NAME")?.Trim() ?? "Playwright TA Report";
        }

        public static string GetPlaywrightDeviceName()
        {
            return Environment.GetEnvironmentVariable("PLAYWRIGHT_DEVICE_NAME")?.Trim() ?? string.Empty;
        }

        public static void SetPlaywrightDeviceName(string deviceName)
        {
            Environment.SetEnvironmentVariable("PLAYWRIGHT_DEVICE_NAME", deviceName);
        }

        public static ViewportSize GetDefaultViewportSize()
        {
            var width = Environment.GetEnvironmentVariable("VIEWPORT_SIZE_WIDTH")?.Trim() ?? "1280";
            var height = Environment.GetEnvironmentVariable("VIEWPORT_SIZE_HEIGHT")?.Trim() ?? "800";

            return new ViewportSize
            {
                Width = int.Parse(width),
                Height = int.Parse(height)
            };
        }

        public static void SetDefaultViewportSize(int width, int height)
        {
            Environment.SetEnvironmentVariable("VIEWPORT_SIZE_WIDTH", width.ToString());
            Environment.SetEnvironmentVariable("VIEWPORT_SIZE_HEIGHT", height.ToString());
        }

        public static bool GetCaptureBrowserLogs()
        {
            return Convert.ToBoolean(Environment.GetEnvironmentVariable("CAPTURE_BROWSER_LOGS"));
        }
    }
}
