using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectPatternTests.PageObjects
{
    class HomePage
    {
        private IWebDriver driver;
        private string url = @"https://www.bbc.com";
        public HomePage(IWebDriver browser)
        {
            this.driver = browser;
            this.driver.Manage().Window.Maximize();
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='orb-nav-links']/ul/li[2]/a")]
        public IWebElement NewsLink { get; set; }
        public void goToPage()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }
        public NewsPage goToNewsPage()
        {
            this.NewsLink.Click();
            return new NewsPage(this.driver);
        }
    }
}