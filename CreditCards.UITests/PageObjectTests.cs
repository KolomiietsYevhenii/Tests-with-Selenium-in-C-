using System.Collections.ObjectModel;
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

                ReadOnlyCollection<IWebElement> informationsElemnents = jobPage.HeaderLiElements;

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
                jobPage.NavigateToTab(jobPage.JobsTabElement);
                jobPage.NavigateToTab(jobPage.TrendsTabElement);

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
                var homepage = new HomePage(driver);

                driver.MaximizeWindow();
                jobPage.NavigateTo();
                string initialUsersCount = jobPage.UsersCount;
                
                homepage.NavigateTo();
                driver.Navigate().Back();

                string reloadedUsers = jobPage.UsersCount;
                Assert.NotEqual(initialUsersCount, reloadedUsers);
            }
        }

        [Fact]
        public void СheckJobSearchField()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var jobPage = new JobPage(driver);
                jobPage.NavigateTo();

                jobPage.SelectJobCategory("QA");
                jobPage.FillJobSearchField("Luxoft");
                jobPage.ClickJobSearchButton();

                Assert.Contains("Luxoft", driver.FindElement(By.CssSelector("a.company")).Text);
            }
        }
    }
}
