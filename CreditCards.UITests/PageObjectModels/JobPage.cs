using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CreditCards.UITests.PageObjectModels
{
    public class JobPage
    {
        private const    string     PageUrl   = "https://jobs.dou.ua/";
        private const    string     PageTitle = "Вакансии | DOU";
        private readonly IWebDriver _driver;

        public JobPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ReadOnlyCollection<IWebElement> HeaderLiElements => _driver.FindElements(By.CssSelector(".sub li"));
        public IWebElement JobsTabElement => _driver.FindElements(By.CssSelector(".sub li")).FirstOrDefault();
        public IWebElement TrendsTabElement => _driver.FindElements(By.CssSelector(".sub li")).FirstOrDefault();

        public  string UsersCount => _driver.FindElement(By.CssSelector("\\span.regcount")).Text;

        public void ClickSearchFooterLink()
        {
            _driver.FindElement(By.PartialLinkText("Джинне")).Click();
        }

        //public void SelectCategory() => Driver.FindElement(By.CssSelector("select"));

        public void FillJobSearchField(string inputValue)
        {
            _driver.FindElement(By.CssSelector("input.job")).SendKeys(inputValue);
        }

        public void ClickJobSearchButton()
        {
            _driver.FindElement(By.CssSelector("input.btn-search")).Click();
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(PageUrl);
        }

        public void NavigateToTab(IWebElement tab)
        {
           tab.Click();
        }

        public void EnsurePageLoaded()
        {
            bool pageHasLoaded = (_driver.Url == PageUrl) && (_driver.Title == PageTitle);
            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{_driver.Url}' Page Source: \r\n {_driver.PageSource}");
            }
        }

        public void SelectJobCategory(string value)
        {
            var selectJobElement = _driver.FindElement(By.Name("education"));
            //create select element object 
            var selectElement = new SelectElement(selectJobElement);

            //select by value
            //selectElement.SelectByValue("Jr.High");
            // select by text
            selectElement.SelectByText(value);
        }
    }
}
