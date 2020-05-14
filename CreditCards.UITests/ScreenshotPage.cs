using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class ScreenshotPage
    {
        [Fact]
        public void ScreenshotOfPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://dou.ua/");
                driver.Manage().Window.FullScreen();
                ITakesScreenshot screenshotdriver = (ITakesScreenshot) driver;
                Screenshot screenshots = screenshotdriver.GetScreenshot();
                screenshots.SaveAsFile("homepage.png", ScreenshotImageFormat.Png);
            }
        }
    }
}
