using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingDriver
{
    [TestFixture]
    public class DriverTests
    {
        private IWebDriver browser;
        [SetUp]
        public void OpenBrowserAndGoToWebSite()
        {
            browser = new ChromeDriver();
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
            browser.Manage().Window.Maximize();
            browser.Navigate().GoToUrl("http://gsv.aero");
        }


        //Тест-кейс №3: Перевести веб-приложение с русского языка на английский
        //1) Зайти на сайт https://gsv.aero
        //2) Нажать сверху на кнопку "EN"
        //Фактический результат: объекты на сайте(меню, различные кнопки и т.д.) переведены на английский; переведено не 
        [Test]
        public void TranslateWebSiteToEnglish()
        {
            string translateButtonUrl = browser.FindElement(By.TagName("body")).FindElement(By.TagName("header")).FindElement(By.ClassName("header-inner"))
                .FindElement(By.ClassName("container")).FindElement(By.ClassName("header-row"))
                .FindElements(By.TagName("div"))[3].FindElement(By.TagName("a"))
                .GetAttribute("href");
            browser.Navigate().GoToUrl(translateButtonUrl);
            Assert.IsTrue(browser.FindElement(By.TagName("html")).GetAttribute("lang") == "en");
        }

        //Тест-кейс №8: Перейти в окно покупки билета через поиск
        //1) Зайти на сайт https://gsv.aero
        //2) Нажать на кнопку лупы в верхней части экрана
        //3) Ввести "Купить билет" в текстовом поле
        //4) Нажать кнопку "Поиск"
        //Фактический результат: список вариантов, подходящие под запрос поиска.
        [Test]
        public void BuyTicketThroughSearchWindow()
        {
            string searchButtonMainWindowUrl = browser.FindElement(By.TagName("body")).FindElement(By.TagName("header")).FindElement(By.ClassName("header-inner"))
               .FindElement(By.ClassName("container")).FindElement(By.ClassName("header-row"))
               .FindElements(By.TagName("div"))[3].FindElements(By.TagName("a"))[1]
               .GetAttribute("href");
            browser.Navigate().GoToUrl(searchButtonMainWindowUrl);

            browser.FindElement(By.Name("q")).SendKeys("Купить билеты");
            IWebElement searchButtonSearchWindow = browser.FindElement(By.TagName("section")).FindElement(By.ClassName("container"))
                .FindElement(By.TagName("form")).FindElement(By.ClassName("search-btn-shell")).FindElement(By.TagName("button"));
            searchButtonSearchWindow.Click();

            browser.Navigate().GoToUrl(browser.FindElement(By.XPath("//a[@class='search-result']")).GetAttribute("href"));
            Assert.IsTrue(browser.Url == "https://gsv.aero/services/booking-tickets/");
        }
        [TearDown]
        public void CloseBrowser()
        {
            browser.Quit();
        }

    }
}
