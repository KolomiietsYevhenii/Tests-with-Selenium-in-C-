using System;
using System.Collections.Generic;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class Guidgenerator
    {
        const string HomeUrl   = "https://www.guidgenerator.com/";
        const string HomeTitle = "Online GUID Generator";

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement clickOnGuidElement =
                    driver.FindElement(By.Id("btnGenerate"));

                clickOnGuidElement.Click();
                IWebElement generationGuidElement = driver.FindElement(By.Id("txtResults"));

                string initialGuid = generationGuidElement.Text;

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                //Assert.Equal(HomeUrl, driver.Url);

                string reloadedGuid = driver.FindElement(By.Id("txtResults")).Text;

                Assert.NotEqual(initialGuid, reloadedGuid);
            }
        }
    }
}