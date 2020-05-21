using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class PageObjectTests
    {
        [Fact]
        public void ChackingSomeElements()

        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var jobPage = new JobPage(driver);
                jobPage.NavigateTo();

                ReadOnlyCollection<IWebElement> informationsElemnents = jobPage.LiElemnents;

                Assert.Equal("Вакансии", informationsElemnents[0].Text);
                Assert.Equal("Тренды", informationsElemnents[1].Text);
                Assert.Equal("Компании", informationsElemnents[2].Text);
                Assert.Equal("Рейтинг", informationsElemnents[3].Text);
                Assert.Equal("Топ-50", informationsElemnents[4].Text);
                Assert.Equal("Отзывы", informationsElemnents[5].Text);
            }
        }

        [Fact]
        public void OpenDjinniFooterLinkInNewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var jobPage = new JobPage(driver);
                jobPage.NavigateTo();

                jobPage.ClickSearchFooterLink();

                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string jobPageTab = allTabs[0];
                string searchDjinniTab = allTabs[1];
                driver.SwitchTo().Window(searchDjinniTab);
                Assert.StartsWith("https://djinni.co/", driver.Url);
            }
        }

        [Fact]

        public void ReloadJobPageOnBack()              
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var jobPage = new JobPage(driver);
                jobPage.NavigateTo();

                string initialUsers = jobPage.GenerationUsers;

                jobPage.NavigateToHomeUrl();
                driver.Navigate().Back();

                string reloadedUsers = jobPage.GenerationUsers;
                Assert.NotEqual(initialUsers, reloadedUsers);
            }
        }

        [Fact]
        public void СheckJobSearchField()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var jobPage = new JobPage(driver);
                jobPage.NavigateTo();

               // jobPage.SelectCategory();
                jobPage.FillJobSearchField();
                jobPage.ClickJobSearchButton();

                Assert.Contains("Luxoft", driver.FindElement(By.CssSelector("a.company")).Text);
            }
        }
    }
}
