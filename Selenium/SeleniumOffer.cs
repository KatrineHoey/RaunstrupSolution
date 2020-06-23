using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Selenium
{
    [TestClass]
    public class SeleniumOffer
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://localhost:44353";
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (System.Exception)
            {
                // throw;
            }
        }

        [TestMethod]
        public void OpretTilbud()
        {
            driver.Navigate().GoToUrl(baseURL + "/Offer/Create");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
            driver.FindElement(By.Id("WorkingTitle")).Click();
            driver.FindElement(By.Id("WorkingTitle")).SendKeys("Nye vinduer");
            driver.FindElement(By.Id("StartDate")).Click();
            driver.FindElement(By.Id("StartDate")).SendKeys("2020-05-25");
            driver.FindElement(By.Id("EndDate")).Click();
            driver.FindElement(By.Id("EndDate")).SendKeys("2020-06-05");
            driver.FindElement(By.Id("Description")).Click();
            driver.FindElement(By.Id("Description")).SendKeys("Velux vinduer");
            driver.FindElement(By.CssSelector(".btn-primary")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement page = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("page")));
            string MsText = page.Text;

            Assert.AreEqual("Alle tilbud", MsText);
        }

        //[TestMethod]
        //public void TilknytMedarbejder()
        //{
        //    driver.Navigate().GoToUrl(baseURL + "/Offer/Details/1");
        //    driver.Manage().Window.Maximize();
        //    Thread.Sleep(10000);
        //    driver.FindElement(By.Id("kunde")).Click();

        //    driver.FindElement(By.XPath("//a[@href='/Customers/GetOfferCustomer/1'])")).Click();
        //    Thread.Sleep(30);
        //    driver.FindElement(By.LinkText("Tilknyt")).Click();
        //}

        [TestMethod]
        public void OpretKunde()
        {
            driver.Navigate().GoToUrl(baseURL + "/Customers");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
           
            driver.FindElement(By.LinkText("Opret ny kunde")).Click();
            driver.FindElement(By.Id("Name")).Click();
            driver.FindElement(By.Id("Name")).SendKeys("anders and");
            driver.FindElement(By.Id("PhoneNo")).Click();
            driver.FindElement(By.Id("PhoneNo")).SendKeys("66141448");
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("anders@anders.dk");
            driver.FindElement(By.Id("Address")).Click();
            driver.FindElement(By.Id("Address")).SendKeys("andeby 33");
            driver.FindElement(By.Id("City")).Click();
            driver.FindElement(By.Id("City")).SendKeys("vejle");
            driver.FindElement(By.Id("DiscountGroup")).Click();
            driver.FindElement(By.Id("DiscountGroup")).SendKeys("1");
            driver.FindElement(By.Id("DiscountGroup")).Click();
            driver.FindElement(By.CssSelector(".btn-primary")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));

            IWebElement webElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("name")));
            string MsText = webElement.Text;

            Assert.AreEqual("anders and", MsText);
        }
    }
}



