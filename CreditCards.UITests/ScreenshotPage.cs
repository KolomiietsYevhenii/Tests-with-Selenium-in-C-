//using OpenQA.Selenium;
//using Xunit;

//namespace CreditCards.UITests
//{
//    public class ScreenshotPage
//    {
//        private readonly IWebDriver _driver;

//        public ScreenshotPage(IWebDriver driver)
//        {
//            _driver = driver;
//        }

//        [Fact]
//        public void ScreenshotOfPage()
//        {
//            ITakesScreenshot screenshotdriver = (ITakesScreenshot) _driver;
//            Screenshot screenshots = screenshotdriver.GetScreenshot();
//            screenshots.SaveAsFile("homepage.png", ScreenshotImageFormat.Png);
//        }
//    }
//}
