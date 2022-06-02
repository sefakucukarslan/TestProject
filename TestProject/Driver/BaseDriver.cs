using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Driver
{
    public class BaseDriver
    {
        public IWebDriver driver;

        public void OpenPage(string url)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        public void QuitPage()
        {
            driver.Quit();
        }
    }
}
