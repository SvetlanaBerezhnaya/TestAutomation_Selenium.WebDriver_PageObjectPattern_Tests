using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectPatternTests.PageObjects
{
    class NewsPage
    {
        private IWebDriver driver;
        public NewsPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'More')]")]
        public IWebElement More_theSecondLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='orb-modules']/header/div[2]/div/div[2]/div/div/ul[4]/li/a/span")]
        public IWebElement HaveYourSay_link { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-entityid='container-top-stories#1']/div/div/a")]
        public IWebElement TopStory_link { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='orb-search-q']")]
        public IWebElement Search_input { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='orb-search-button']")]
        public IWebElement Search_button { get; set; }
        public void expand_More_dropdownList()
        {
            this.More_theSecondLink.Click();
        }
        public HaveYourSayPage click_HaveYourSay_link()
        {
            this.HaveYourSay_link.Click();
            return new HaveYourSayPage(this.driver);
        }
        public string getText_TopStory_link(short numberOfTopStory)
        {
            //IWebElement TopStory_link = this.driver.FindElement(By.XPath("//*[@data-entityid='container-top-stories#" + numberOfTopStory + "']/div/div/a"));
            IWebElement TopStory_link = this.driver.FindElement(By.XPath("//*[@data-entityid='container-top-stories#" + numberOfTopStory + "']"));
            return TopStory_link.Text;
        }
        public string getText_TopStoryCategory_link(short numberOfTopStory)
        {
            IWebElement TopStoryCategory_link = this.driver.FindElement(By.XPath("//*[@data-entityid='container-top-stories#" + numberOfTopStory + "']/div/ul/li[2]/a"));
            return TopStoryCategory_link.Text;
        }
        public void Search(string toSearch)
        {
            this.Search_input.Clear();
            this.Search_input.SendKeys(toSearch);
        }
        public SearchResultsPage goToSearchResultsPage()
        {
            this.Search_button.Click();
            return new SearchResultsPage(this.driver);
        }
    }
}