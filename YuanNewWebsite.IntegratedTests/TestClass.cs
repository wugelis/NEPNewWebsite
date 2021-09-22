// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace EPAEYuanNewWebsite.IntegratedTests
{
    [TestFixture]
    public class ErNetAdminLogin
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                //driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheErNetAdminLoginTest()
        {
            driver.Navigate().GoToUrl("http://localhost:5404/EPAEYuanAdmin/ErNetAdmin/Login");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("LoginId")).Click();
            driver.FindElement(By.Id("LoginId")).Clear();
            driver.FindElement(By.Id("LoginId")).SendKeys("admin");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("ErNet.PassWord");
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='密碼：'])[1]/following::input[2]")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("檔案上傳")));

            Thread.Sleep(2000);

            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();

            driver.FindElement(By.LinkText("公告檔案上傳更新")).Click();

            Thread.Sleep(2000);

            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Choose File...'])[2]/following::input[2]")).Click();

        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
