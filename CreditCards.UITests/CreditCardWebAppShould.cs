using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        const         string HomeUrl         = "https://dou.ua/";
        const         string JobsUrl         = "https://jobs.dou.ua/";
        const         string HomeTitle       = "Сообщество программистов | DOU";
        private const string RegisterCompany = "https://jobs.dou.ua/register/";

        private readonly ITestOutputHelper _output;

        public CreditCardWebAppShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                //var emailInput = driver.FindElement(By.Id("username"));

                //emailInput.SendKeys("test");

                //emailInput.Clear();

                //emailInput.Click();

                //Assert.True(emailInput.Displayed);


                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(JobsUrl);
                
                //driver.Navigate().Refresh();

               // Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(JobsUrl, driver.Url);

                driver.MakeScreenshot("ReloadHomePage");
                //new ScreenshotPage(driver).ScreenshotOfPage();
                //ScreenshotPage s = new ScreenshotPage();
                





            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                string initialUsers = driver.FindElement(By.ClassName("regcount")).Text;

                DemoHelper.Pause();
                driver.Navigate().GoToUrl(JobsUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedUsers = driver.FindElement(By.ClassName("regcount")).Text;

                Assert.NotEqual(initialUsers, reloadedUsers);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(JobsUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void SlideArticlesByPartialLinkTextExplisityWait()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {HomeUrl}");
                driver.Navigate().GoToUrl(HomeUrl);

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explisity wait");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                IWebElement applyLink =
                    wait.Until((d) => d.FindElement(By.PartialLinkText("Как оформить профиль на GitHub")));

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed = {applyLink.Enabled}");
                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");

                applyLink.Click();

                //IWebElement fourthSliderArticleElement =
                //    driver.FindElement(By.PartialLinkText("Как оформить профиль на GitHub"));
                //fourthSliderArticleElement.Click();

                // Assert.Equal(HomeTitle, driver.Title);
                // Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void JobsPageSearchButtonByClass()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(JobsUrl);
                //DemoHelper.Pause();

                //IWebElement searchFieldElement =
                //    driver.FindElement(By.Name("search"));
                //searchFieldElement.SendKeys("Luxoft");

                IWebElement homeButtonWithLinqByText = driver.FindElements(By.CssSelector(".b-head ul > li"))
                    .FirstOrDefault(x => x.Text == "ГЛАВНАЯ");



                //TODO By.TagName("li") - really bad selector, try something more specific 
                ReadOnlyCollection<IWebElement> searchButtonElements =
                    driver.FindElements(By.TagName("li"));

                //TODO wouldn't not work with 'uk' or 'en' localization 
                Assert.Equal("ГЛАВНАЯ", homeButtonWithLinqByText?.Text); //searchButtonElements[1] will return exception if searchButtonElements is empty 

                //searchbuttonElement.Click();

                // Assert.Equal(HomeTitle, driver.Title);
                // Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        public void HomePageSearchByXpath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {HomeUrl}");
                driver.Navigate().GoToUrl(HomeUrl);

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                //TODO:  By.ClassName("top) - more flexible and readable
                IWebElement searchByTagElement =  driver.FindElement(By.XPath("//a [text() [contains(.,'Топ-50')]]"));
                
                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed");
                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                searchByTagElement.Click();

                //Where is assert? 
                Assert.Equal("https://dou.ua/lenta/articles/top-50-jan-2020/?from=doufp", driver.Url);
            }
        }

        [Fact]
        public void SlideArticlesByPartialLinkTextPrebuildConditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(4000));
                IWebElement applyLink =
                    wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.PartialLinkText("IT-фахівці розвивають українськомовний YouTube")));

                applyLink.Click();

                //Where is assert? 
                Assert.Equal("https://dou.ua/lenta/articles/how-it-specialists-develop-ukrainian-youtube/", driver.Url);
            }
        }

        [Fact]
        public void BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(RegisterCompany);
                driver.Manage().Window.FullScreen();

                driver.FindElement(By.Id("id_name")).SendKeys("LLC Agrobiohimproduct");
                driver.FindElement(By.Id("id_description")).SendKeys(" The main trichogram supplier in The world");
                driver.FindElement(By.Id("id_site")).SendKeys("http://trihogramma.kiev.ua/");

                //my solution

                //driver.FindElement(By.Id("id_employees")).Click();
                //driver.FindElement(By.XPath("//*[@id='id_employees']/option[7]")).Click();

                IWebElement logoUpload = driver.FindElement(By.Name("logo"));
                logoUpload.SendKeys("D:\\Download\\logo.gif");

                IWebElement   employeesSelectElement = driver.FindElement(By.Id("id_employees"));
                SelectElement idEmployees            = new SelectElement(employeesSelectElement);

                //Assert.Equal("0", IdEmployees.SelectedOption.Text);
                //foreach (IWebElement option in IdEmployees.Options)
                //{
                //    output.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
                //}

                idEmployees.SelectByValue("7");

                idEmployees.SelectByText("800—1500");

                idEmployees.SelectByIndex(1);

                driver.FindElement(By.ClassName("g-btn-save")); //.Click;

                //DemoHelper.Pause();

                //Where is assert? 
            }
        }

        [Fact]
        public void ResizingOfTheWindow()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                driver.Manage().Window.Maximize();
                DemoHelper.Pause();
                driver.Manage().Window.Minimize();
                DemoHelper.Pause();
                driver.Manage().Window.Size = new System.Drawing.Size(200, 400);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                DemoHelper.Pause();
            }
        }

        [Fact]
        public void CssSelectorCitrus()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.citrus.ua/");

                driver.FindElement(
                    By.CssSelector("div.auth a.link-to")).Click();
                DemoHelper.Pause();

                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                //IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                //DemoHelper.Pause();
                //IAlert alert = driver.SwitchTo().Alert();

                //Assert.Equal("Рад видеть Вас на моем сайте! Пошли дальше?", alert.Text);
                //alert.Accept();
            }
        }

        [Fact]
        public void HandlingPopup()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://htmlweb.ru/java/js1.php");

                //driver.FindElement(
                //By.CssSelector("a.link-to")).Click();

                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                //IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                DemoHelper.Pause();
                IAlert alert = driver.SwitchTo().Alert();

                Assert.Equal("Рад видеть Вас на моем сайте! Пошли дальше?", alert.Text);
                alert.Accept();
            }
        }
    }
}