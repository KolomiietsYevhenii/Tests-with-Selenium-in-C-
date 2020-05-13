using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;


namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        const string HomeUrl = "https://dou.ua/";
        const string JobsUrl = "https://jobs.dou.ua/";
        const string HomeTitle = "Сообщество программистов | DOU";
        private const string RegisterCompany = "https://jobs.dou.ua/register/";



        private readonly ITestOutputHelper output;

        public CreditCardWebAppShould(ITestOutputHelper output)
        {
            this.output = output;
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


                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]

        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                IWebElement generationUserElement =
                    driver.FindElement(By.ClassName("regcount"));
                string initialUsers = generationUserElement.Text;

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

        public void slideArticlesByPartialLinkTextExplisitywait()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {HomeUrl}");
                driver.Navigate().GoToUrl(HomeUrl);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explisity wait");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                IWebElement applyLink =
                    wait.Until((d) => d.FindElement(By.PartialLinkText("Как оформить профиль на GitHub")));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed = {applyLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");

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

        public void jobsPageSearchButtonByClass()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(JobsUrl);
                //DemoHelper.Pause();

                //IWebElement searchFieldElement =
                //    driver.FindElement(By.Name("search"));
                //searchFieldElement.SendKeys("Luxoft");


                ReadOnlyCollection<IWebElement> searchbuttonElements =
                    driver.FindElements(By.TagName("li"));

                Assert.Equal("ГЛАВНАЯ", searchbuttonElements[1].Text);

                //searchbuttonElement.Click();

                // Assert.Equal(HomeTitle, driver.Title);
                // Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]

        public void homePageSearchByXpath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {HomeUrl}");
                driver.Navigate().GoToUrl(HomeUrl);


                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                IWebElement searcByTagElement =
                    driver.FindElement(By.XPath("//a [text() [contains(.,'Топ-50')]]"));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                searcByTagElement.Click();


            }
        }

        [Fact]

        public void slideArticlesByPartialLinkTextPrebuildConduitions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(4000));
                IWebElement applyLink =
                    wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.PartialLinkText("Як і навіщо IT - фахівці розвивають")));
                applyLink.Click();

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


                IWebElement employeesSelectElement = driver.FindElement(By.Id("id_employees"));
                SelectElement IdEmployees = new SelectElement(employeesSelectElement);

                //Assert.Equal("0", IdEmployees.SelectedOption.Text);
                //foreach (IWebElement option in IdEmployees.Options)
                //{
                //    output.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
                //}

                IdEmployees.SelectByValue("7");

                IdEmployees.SelectByText("800—1500");

                IdEmployees.SelectByIndex(1);

                driver.FindElement(By.CssSelector("input.g-btn-save.__long")); //.Click;

                //DemoHelper.Pause();
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
