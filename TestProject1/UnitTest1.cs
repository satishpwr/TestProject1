using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
       private IWebDriver _webDriver;

        [SetUp]
               
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void Test1()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:60005/");//44310
            Thread.Sleep(3000);
            IWebElement enter = _webDriver.FindElement(By.TagName("a"));
            enter.Click();

            Thread.Sleep(3000);
            TestEmployeeListScreen(_webDriver, "Launch");
            Thread.Sleep(7000);

            TestAddEmployeeNew(_webDriver);
            TestEmployeeListScreen(_webDriver, "Add");
            Thread.Sleep(3000);

            TestEditEmployee(_webDriver);
            TestEmployeeListScreen(_webDriver, "Add");
            Thread.Sleep(3000);

            TestDeleteEmployees(_webDriver);
            TestEmployeeListScreen(_webDriver, "Add");
            Thread.Sleep(3000);

            TestAddEmployeeExist(_webDriver);
            TestEmployeeListScreen(_webDriver, "Add");
            Thread.Sleep(3000);

            //_webDriver.FindElement(By.XPath("//div/a[text()='About']")).Click();
            // String note = _webDriver.FindElement(By.XPath("//h1/[text()='Subscribe to our Newsletter']")).Text;
            //Thread.Sleep(3000);
            //Assert.AreEqual("Subscribe to our Newsletter", note);
        }


        public void TestEmployeeListScreen(IWebDriver driver, string type)
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            WebDriverWait wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3));
            
            wait.Until((d) => { return d.Title.ToLower().StartsWith("employeelist"); });
            IWebElement addLink = driver.FindElement(By.LinkText("Add"));
            IWebElement delLink = driver.FindElement(By.Id("Delete"));
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            //ss.SaveAsFile(ScreenShotLocation + "\\" + type + "EmpList.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private void TestAddEmployeeNew(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Add")).Click();
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            WebDriverWait wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(3));

            wait.Until((d) => { return d.FindElement(By.Id("Name")); });

            IWebElement name = driver.FindElement(By.Id("Name"));
            IWebElement sal = driver.FindElement(By.Id("Sal"));
            IWebElement submit = driver.FindElement(By.Id("Submit"));

            name.SendKeys("Daniel");
            Thread.Sleep(3000);
            sal.SendKeys("5000");
            Thread.Sleep(3000);

            submit.Click();
            Thread.Sleep(3000);
            IAlert alert = null;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { alert = d.SwitchTo().Alert(); return alert; });
            alert.Accept();

        }

        private void TestAddEmployeeExist(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Add")).Click();
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            WebDriverWait wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(3));
            
            wait.Until((d) => { return d.FindElement(By.Id("Name")); });

            IWebElement name = driver.FindElement(By.Id("Name"));
            IWebElement sal = driver.FindElement(By.Id("Sal"));
            IWebElement submit = driver.FindElement(By.Id("Submit"));

            name.SendKeys("Johnson");
            Thread.Sleep(3000);
            sal.SendKeys("5000");
            Thread.Sleep(3000);

            submit.Click();
            Thread.Sleep(3000);
            IAlert alert = null;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { alert = d.SwitchTo().Alert(); return alert; });
            alert.Accept();
            
        }

       

        private void TestEditEmployee(IWebDriver driver)
        {
            //Find the element anchortag for the employee having name as 'Anderson'
            IWebElement anchorTag = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("return $('a',$(\"td:contains('Anderson')\").parent())[0]");
            Thread.Sleep(3000);
            anchorTag.Click();
            
            //Wait and then check until the control with id=Name is available
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            WebDriverWait wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3));
            Thread.Sleep(3000);
            wait.Until((d) => { return d.FindElement(By.Id("Name")); });

            //Find all the lements
            IWebElement name = driver.FindElement(By.Id("Name"));
            IWebElement sal = driver.FindElement(By.Id("Sal"));
            IWebElement submit = driver.FindElement(By.Id("Submit"));

            //Set the data (Change the name)
            name.SendKeys(" James");
            Thread.Sleep(3000);
            submit.Click();

            IAlert alert = null;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { alert = d.SwitchTo().Alert(); return alert; });
            alert.Accept();
        }

        private void TestDeleteEmployees(IWebDriver driver)
        {
            //Find the last employee
            IWebElement checkBox = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("return $('input:checkbox:last')[0]");
            checkBox.Click();

            //Find delete button and click
            IWebElement delBtn = driver.FindElement(By.Id("Delete"));
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            WebDriverWait wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3));
            Thread.Sleep(3000);
            delBtn.Click(); //Perfome delete operation


        }


    }
}