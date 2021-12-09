using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void Test1()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:44310/");
            IWebElement enter = _webDriver.FindElement(By.TagName("a"));
            enter.Click();
            //_webDriver.FindElement(By.XPath("//div/a[text()='About']")).Click();
            // String note = _webDriver.FindElement(By.XPath("//h1/[text()='Subscribe to our Newsletter']")).Text;
            //Thread.Sleep(3000);
            //Assert.AreEqual("Subscribe to our Newsletter", note);
        }
    }
}