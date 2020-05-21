using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModels
{
    public class JobPage
    {
        private const string PageUrl = "https://jobs.dou.ua/";
        const string HomeUrl = "https://dou.ua/";
        private const string PageTitle = "Вакансии | DOU";
        private readonly IWebDriver Driver;

        public JobPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public ReadOnlyCollection<IWebElement> LiElemnents => Driver.FindElements(By.CssSelector(".sub li"));

        public  string GenerationUsers => Driver.FindElement(By.CssSelector("\\span.regcount")).Text;

        public void ClickSearchFooterLink() => Driver.FindElement(By.PartialLinkText("Джинне")).Click();

        //public void SelectCategory() => Driver.FindElement(By.CssSelector("select"));

        public void FillJobSearchField() => Driver.FindElement(By.CssSelector("input.job")).SendKeys("Luxoft");

        public void ClickJobSearchButton() => Driver.FindElement(By.CssSelector("input.btn-search")).Click();

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded();
        }

        public void NavigateToHomeUrl()
        {
            Driver.Navigate().GoToUrl(HomeUrl);
            Driver.Manage().Window.Maximize();
        }

        public void EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == PageUrl) && (Driver.Title == PageTitle);
            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }
    }
}
