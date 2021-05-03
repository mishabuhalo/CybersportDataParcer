using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace CybersportDataParser.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://www.hltv.org/matches");
        }
    }
}
