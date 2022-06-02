using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using TestProject.Driver;

namespace TestProject
{
    [TestClass]
    public class UnitTest1 : BaseDriver
    {

        [TestMethod]
        public void CreatNewUser()
        {
            OpenPage("http://www.uitestpractice.com/Students/Index");

            IWebElement createNew = driver.FindElement(By.XPath("//button[@class='btn btn-info']"));
            createNew.Click();
            Thread.Sleep(2000);
            IWebElement firstName = driver.FindElement(By.XPath("//input[@id='FirstName']"));
            firstName.SendKeys("Sefa");
            Thread.Sleep(2000);
            IWebElement lastName = driver.FindElement(By.XPath("//input[@id='LastName']"));
            lastName.SendKeys("Küçükarslan");
            Thread.Sleep(2000);
            IWebElement date = driver.FindElement(By.XPath("//input[@id='EnrollmentDate']"));
            date.SendKeys("05/30/2022");
            Thread.Sleep(2000);
            IWebElement createButton = driver.FindElement(By.XPath("//input[@value='Create']"));
            createButton.Click();

            IWebElement searchName = driver.FindElement(By.XPath("//input[@id='Search_Data']"));
            searchName.Click();
            searchName.SendKeys("Sefa");
            IWebElement find = driver.FindElement(By.XPath("//input[@value='Find']"));
            find.Click();

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//td"));
            Assert.AreEqual(elements[0].Text, "Sefa");
            Assert.AreEqual(elements[1].Text, "Küçükarslan");
            Assert.AreEqual(elements[2].Text, "5/30/2022 12:00:00 AM");

            QuitPage();
        }
        
        [TestMethod]
        public void AjaxCall()
        {
            OpenPage("http://www.uitestpractice.com/Students/Contact");

            IWebElement linkText = driver.FindElement(By.PartialLinkText("This is"));
            linkText.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.ClassName("ContactUs")));
            string result = driver.FindElement(By.XPath("//div[@class='ContactUs']")).Text;
            Assert.IsTrue(result.Contains("C#"));

            QuitPage();
        }
        
        [TestMethod]
        public void Form()
        {
            OpenPage("http://www.uitestpractice.com/Students/Form");

            IWebElement firstName = driver.FindElement(By.XPath("//input[@id='firstname']"));
            firstName.Click();
            firstName.SendKeys("Sefa");

            IWebElement lastName = driver.FindElement(By.XPath("//input[@id='lastname']"));
            lastName.Click();
            lastName.SendKeys("Küçükarslan");

            IWebElement country = driver.FindElement(By.XPath("//select[@id='sel1']"));
            SelectElement select = new SelectElement(country);
            select.SelectByText("Iceland");

            IWebElement month = driver.FindElement(By.XPath("//select[@class='ui-datepicker-month']"));
            SelectElement selectMonth = new SelectElement(month);
            selectMonth.SelectByText("Feb");
            
            IWebElement year = driver.FindElement(By.XPath("//select[@class='ui-datepicker-year']"));
            SelectElement selectYear = new SelectElement(year);
            selectYear.SelectByText("1996");
            
            IWebElement day = driver.FindElement(By.XPath("//a[@class='ui-state-default' and text()='10']"));
            day.Click();
            
            IWebElement number = driver.FindElement(By.XPath("//input[@id='phonenumber']"));
            number.Click();
            number.SendKeys("05998887766");
            
            IWebElement userName = driver.FindElement(By.XPath("//input[@id='username']"));
            userName.Click();
            userName.SendKeys("Sefa96");
            
            IWebElement email = driver.FindElement(By.XPath("//input[@id='email']"));
            email.Click();
            email.SendKeys("sefa@abc.com");
            
            IWebElement textArea = driver.FindElement(By.XPath("//textarea[@id='comment']"));
            textArea.Click();
            textArea.SendKeys("This is a secret");
            
            IWebElement psswrd = driver.FindElement(By.XPath("//input[@id='pwd']"));
            psswrd.Click();
            psswrd.SendKeys("123456789");
            
            IWebElement submit = driver.FindElement(By.XPath("//button[@type='submit']"));
            submit.Click();            

            QuitPage();
        }
        
        [TestMethod]
        public void Actions()
        {
            OpenPage("http://www.uitestpractice.com/Students/Actions");

            Actions action = new Actions(driver);
            IWebElement click = driver.FindElement(By.XPath("//button[@name='click']"));
            action.Click(click).Build().Perform();
            driver.SwitchTo().Alert().Accept();
            IWebElement doubleClick = driver.FindElement(By.XPath("//button[@name='dblClick']"));
            action.DoubleClick(doubleClick).Build().Perform();
            driver.SwitchTo().Alert().Accept();
            IWebElement drag = driver.FindElement(By.XPath("//div[@id='draggable']"));
            IWebElement drop = driver.FindElement(By.XPath("//div[@id='droppable']"));
            action.DragAndDrop(drag, drop).Build().Perform();
            IWebElement move = driver.FindElement(By.XPath("//div[@id='div2']"));
            action.MoveToElement(move).Build().Perform();
            IWebElement one = driver.FindElement(By.XPath("//li[@name='one']"));
            IWebElement twelve = driver.FindElement(By.XPath("//li[@name='twelve']"));
            action.ClickAndHold(one).Release(twelve).Build().Perform();

            QuitPage();
        }
        
        [TestMethod]
        public void SwitchTo()
        {
            OpenPage("http://www.uitestpractice.com/Students/Switchto");

            IWebElement alertClick = driver.FindElement(By.XPath("//button[@id='alert']"));
            alertClick.Click();
            driver.SwitchTo().Alert().Accept();
            string textAlert = driver.FindElement(By.XPath("//div[@id='demo']")).Text;
            Assert.IsTrue(textAlert.Contains("You have clicked on ok button in alert window"));
            Thread.Sleep(3000);

            IWebElement confirmAlert = driver.FindElement(By.XPath("//button[@id='confirm']"));
            confirmAlert.Click();
            driver.SwitchTo().Alert().Accept();
            string confirmText = driver.FindElement(By.XPath("//div[@id='demo']")).Text;
            Assert.AreEqual(confirmText, "You pressed Ok in confirm window");
            Thread.Sleep(2000);

            confirmAlert.Click();
            driver.SwitchTo().Alert().Dismiss();
            confirmText = driver.FindElement(By.XPath("//div[@id='demo']")).Text;
            Assert.AreEqual(confirmText, "You pressed Cancel in confirm window");

            IWebElement promptClick = driver.FindElement(By.XPath("//button[@id='prompt']"));
            promptClick.Click();
            driver.SwitchTo().Alert().SendKeys("Sefa");
            driver.SwitchTo().Alert().Accept();
            confirmText = driver.FindElement(By.XPath("//div[@id='demo']")).Text;
            Assert.AreEqual(confirmText, "Hello Sefa! How are you today?");

            driver.SwitchTo().Frame("iframe_a");
            IWebElement name = driver.FindElement(By.XPath("//input[@id='name']"));
            name.Click();
            name.SendKeys("Sefa");
            driver.SwitchTo().ParentFrame();

            IWebElement linkText = driver.FindElement(By.PartialLinkText("Opens in"));
            linkText.Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();

            QuitPage();
        }
        
        [TestMethod]
        public void Select()
        {
            OpenPage("http://www.uitestpractice.com/Students/Select");

            Thread.Sleep(3000);
            IWebElement element = driver.FindElement(By.XPath("//select[@id='countriesSingle']"));
            SelectElement select = new SelectElement(element);
            IList<IWebElement> elements = select.Options;
            Console.WriteLine(elements.Count);

            foreach (var item in elements)
            {
                Console.WriteLine(item.Text);
            }

            Thread.Sleep(3000);
            IWebElement multipleSelect = driver.FindElement(By.XPath("//select[@id='countriesMultiple']"));
            SelectElement multiSelect = new SelectElement(multipleSelect);
            multiSelect.SelectByIndex(1);
            multiSelect.SelectByIndex(3);

            multiSelect.DeselectAll();
            IList<IWebElement> multiElements = multiSelect.AllSelectedOptions;
            Console.WriteLine(multiElements.Count);

            Thread.Sleep(3000);
            IWebElement countries = driver.FindElement(By.XPath("//button[@id='dropdownMenu1']"));
            countries.Click();
            driver.FindElement(By.XPath("//a[text()='England']")).Click();

            string selectedCountry = driver.FindElement(By.XPath("//button[@id='dropdownMenu1']")).Text;
            Console.WriteLine(selectedCountry);
            Thread.Sleep(3000);


            QuitPage();
        }

    }
}
