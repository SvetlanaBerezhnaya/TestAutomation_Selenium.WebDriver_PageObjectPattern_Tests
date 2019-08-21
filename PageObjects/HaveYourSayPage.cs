using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectPatternTests.PageObjects
{
    class HaveYourSayPage
    {
        private IWebDriver driver;
        private static Dictionary<string, string> values = new Dictionary<string, string>();
        public HaveYourSayPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='no-mpu']/div/div[1]/div/div[1]/div/div[2]/div[1]/a")]
        public IWebElement DoYouHaveAquestionForBBCnews_link { get; set; }

        [FindsBy(How = How.ClassName, Using = "text-input--long")]
        public IWebElement WhatQuestionsWouldYouLikeUsToInvestigate_textarea { get; set; }
        public IWebElement Name_input { get; set; }
        public IWebElement EmailAddress_input { get; set; }
        public IWebElement Age_input { get; set; }
        public IWebElement Postcode_input { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[starts-with(@id,'hearken-embed-module-3252-')]/div[1]/div[5]/label/input")]
        public IWebElement SignMeUpForBBCNewsDaily_checkbox { get; set; }
        public IWebElement Submit_button { get; set; }
        public IWebElement Name_label { get; set; }
        public IWebElement EmailAddress_label { get; set; }
        public static string GetTextXpath(string elementLabel, int elementNumber)
        {
            string TextXpath = "(//button[text()='" + elementLabel + "'])[" + elementNumber + "]";
            return TextXpath;
        }
        public static string GetFormXpath(string fieldName)
        {
            string FormXpath = ".//*[@placeholder='" + fieldName + "']";
            return FormXpath;
        }
        public static string GetErrorLabelXpath(string errorLabel)
        {
            string FormXpath = "//div[@class='input-error-message' and contains(text(), '" + errorLabel + "')]";
            return FormXpath;
        }
        public void FillForm(Dictionary<string, string> values, bool click_Submit_button)
        {
            IWebElement Element;

            HomePage homePage = new HomePage(this.driver);
            homePage.goToPage();

            NewsPage newsPage = homePage.goToNewsPage();
            newsPage.expand_More_dropdownList();

            HaveYourSayPage haveYourSayPage = newsPage.click_HaveYourSay_link();
            haveYourSayPage.click_DoYouHaveAquestionForBBCnews_link();

            foreach (KeyValuePair<string, string> keyValue in values)
            {
                Element = driver.FindElement(By.XPath(GetFormXpath(keyValue.Key)));
                Element.SendKeys(keyValue.Value);
            }
            this.click_SignMeUpForBBCNewsDaily_checkbox();
            if (click_Submit_button)
            {
                this.click_Submit_button();
                Thread.Sleep(5000);
            }
            else
            {
                try
                {
                    Screenshot image = ((ITakesScreenshot)this.driver).GetScreenshot();
                    image.SaveAsFile("E:/Screenshot.png");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Assert.Fail("Getting screenshot is failed with exception: " + exception);
                }
            }
        }
        public void click_DoYouHaveAquestionForBBCnews_link()
        {
            DoYouHaveAquestionForBBCnews_link.Click();
        }
        public void click_SignMeUpForBBCNewsDaily_checkbox()
        {
            SignMeUpForBBCNewsDaily_checkbox.Click();
        }
        public void click_Submit_button()
        {
            Submit_button = driver.FindElement(By.XPath(GetTextXpath("Submit", 1)));
            Submit_button.Click();
        }
        public string getColor_label(string fieldName)
        {
            IWebElement label = driver.FindElement(By.XPath(GetErrorLabelXpath(fieldName)));
            return label.GetCssValue("color").Trim();
        }
        public string getBorderColor_input(string fieldName)
        {
            IWebElement input = driver.FindElement(By.XPath(GetFormXpath(fieldName)));
            return input.GetCssValue("border-color").Trim();
        }
    }
}