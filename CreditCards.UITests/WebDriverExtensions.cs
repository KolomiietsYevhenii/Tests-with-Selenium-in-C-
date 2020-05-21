using System;
using System.CodeDom;
using System.Drawing;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;


namespace CreditCards.UITests
{
    public static class WebDriverExtensions
    {
        [UseReporter(typeof(BeyondCompare4Reporter))]
        public static void MakeScreenshot(this IWebDriver driver, string fileName)
        {
            ITakesScreenshot screenshotdriver = (ITakesScreenshot) driver;
            Screenshot screenshots = screenshotdriver.GetScreenshot();
            screenshots.SaveAsFile($"{fileName}.png", ScreenshotImageFormat.Png);

            FileInfo file = new FileInfo($"{fileName}.png");
            
        }
    }
}


