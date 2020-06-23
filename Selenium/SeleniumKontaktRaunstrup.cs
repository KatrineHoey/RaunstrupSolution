using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text;

namespace Selenium
{
    [TestClass]
    public class SeleniumKontakt
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
        public void TestMethod1()
        {
            driver.Navigate().GoToUrl(baseURL + "/Contact");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 840);
            driver.FindElement(By.Id("Name")).Click();
            driver.FindElement(By.Id("Name")).SendKeys("hans hansen");
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("hans@hans.dk");
            driver.FindElement(By.Id("Subject")).Click();
            driver.FindElement(By.Id("Subject")).SendKeys("dør");
            driver.FindElement(By.Id("Message")).Click();
            driver.FindElement(By.Id("Message")).SendKeys("hej. Jeg vil gerne have en ny dør");
            driver.FindElement(By.Id("knap")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            IWebElement alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("alert")));
            string MsText = alert.Text;

            Assert.AreEqual("Tak fordi du kontaktede os, vi vender hurtigst muligt tilbage.", MsText);
        }
    }
}
