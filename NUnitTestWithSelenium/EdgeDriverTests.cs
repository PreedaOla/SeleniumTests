using System;
using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

// Use Test Explorer and run tests or run from command line.
// https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-vstest?tabs=netcore21
// >dotnet vstest EdgeDriverTest.dll

// In order to run the below test(s), 
// please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
// to install Microsoft WebDriver.

namespace EdgeDriverTests
{
    [TestFixture]
    public class StockPageTitle
    {
        private EdgeDriver _driver;
        private List<decimal> _stockPrices;

        [SetUp]
        public void Setup()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);

            _stockPrices = new List<decimal>();
        }
        private void LoadStock(string stock)
        {
            _driver.Url = "https://www.bing.com";
            var element = _driver.FindElementById("sb_form_q");
            element.Clear();
            element.SendKeys(stock);
            element.SendKeys(Keys.Enter);

            Thread.Sleep(3000);

            // Verify page title.
            Assert.AreEqual(stock + " - Bing", _driver.Title);

            element = _driver.FindElementByXPath("//*[@id='Finance_Quote']/div");
            _stockPrices.Add(Convert.ToDecimal(element.Text));

            Thread.Sleep(1000);
        }

        [Property("Priority", 1)]
        [Test]
        public void VerifyPageTitle()
        {
            LoadStock("msft stock");
            LoadStock("sbux stock");
            LoadStock("aapl stock");
            LoadStock("tsla stock");
            LoadStock("kr stock");
            LoadStock("sq stock");
            LoadStock("c stock");
            LoadStock("cmcsa stock");
            LoadStock("t stock");
            LoadStock("s stock");
            LoadStock("vz stock");
            LoadStock("z stock");
            LoadStock("ko stock");
            LoadStock("brkb stock");
            LoadStock("pep stock");
            LoadStock("baba stock");

            foreach (Decimal value in _stockPrices)
            {
                Console.WriteLine(value);
            }
        }

        [Property("Priority", 2)]
        [Test]
        public void NewsReader()
        {
            _driver.Url = "https://www.bing.com";
            var element = _driver.FindElementById("scpl3");
            element.Click();

            Thread.Sleep(1000);

            string[] xPaths = new string[]
            {
                "//*[@id='news']/div[1]/div/ul/li[1]/a",
                "//*[@id='news']/div[1]/div/ul/li[2]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[1]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[2]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[3]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[4]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[5]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[6]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[7]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[8]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[9]/a",
                "//*[@id='news']/div[1]/div/ul/li[3]/ul/li[10]/a",
                "//*[@id='news']/div[1]/div/ul/li[4]/a",
                "//*[@id='news']/div[1]/div/ul/li[5]/a",
                "//*[@id='news']/div[1]/div/ul/li[6]/a",
                "//*[@id='news']/div[1]/div/ul/li[7]/a",
                "//*[@id='news']/div[1]/div/ul/li[8]/a",
                "//*[@id='news']/div[1]/div/ul/li[9]/a",
                "//*[@id='news']/div[1]/div/ul/li[10]/a",
                "//*[@id='news']/div[1]/div/ul/li[12]/a",
                "//*[@id='news']/div[1]/div/ul/li[12]/a"
            };

            foreach (string x in xPaths)
            {
                element = _driver.FindElementByXPath(x);
                element.Click();

                Thread.Sleep(1000);
            }
        }

        [TearDown]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}