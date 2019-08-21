using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectPatternTests.PageObjects
{
    class SearchResultsPage
    {
        private IWebDriver driver;
        public SearchResultsPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@data-result-number='1']/article/div/h1/a")]
        public IWebElement SearchResultNumber1_link { get; set; }
        public string getText_SearchResultNumber1_link()
        {
            return this.SearchResultNumber1_link.Text;
        }
    }
}