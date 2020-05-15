using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    static class WebDriverExtensions
    {
        public static void MakeScreenshot(this IWebDriver driver, string fileName)
        {
            ITakesScreenshot screenshotdriver = (ITakesScreenshot) driver;
            Screenshot screenshots = screenshotdriver.GetScreenshot();
            screenshots.SaveAsFile($"{fileName}.png", ScreenshotImageFormat.Png);
        }
    }
}

//        public static void GetAllElements(this IWebDriver driver)
//        {
//            ITakesScreenshot screenshotdriver = (ITakesScreenshot)driver;
//            Screenshot screenshots = screenshotdriver.GetScreenshot();
//            //screenshots.SaveAsFile($"{fileName}.png", ScreenshotImageFormat.Png);
//        }
//    }
//}
