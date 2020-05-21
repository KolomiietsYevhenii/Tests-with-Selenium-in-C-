using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModels
{
    class HomePage
    {
        private const    string     HomeUrl = "https://dou.ua/";
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(HomeUrl);
        }
    }
}