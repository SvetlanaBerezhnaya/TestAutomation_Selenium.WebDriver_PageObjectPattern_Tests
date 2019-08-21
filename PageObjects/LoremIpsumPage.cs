using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectPatternTests.PageObjects
{
    public class LoremIpsumPage
    {
        private IWebDriver driver;
        private string url = @"https://www.lipsum.com";
        public LoremIpsumPage(IWebDriver browser)
        {
            this.driver = browser;
            this.driver.Manage().Window.Maximize();
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='amount']")]
        public IWebElement AmountInputField { get; set; }

        [FindsBy(How = How.Id, Using = "bytes")]
        public IWebElement BytesRadioButton { get; set; }

        [FindsBy(How = How.Id, Using = "generate")]
        public IWebElement GenerateLoremIpsumButton { get; set; }

        [FindsBy(How = How.Id, Using = "lipsum")]
        public IWebElement LipsumText { get; set; }
        public void GetLipsumText(int amount, out string Lipsum)
        {
            this.driver.Navigate().GoToUrl(this.url);
            this.AmountInputField.Clear();
            this.AmountInputField.SendKeys(amount.ToString());
            this.BytesRadioButton.Click();
            this.GenerateLoremIpsumButton.Click();
            Lipsum = this.LipsumText.Text;
        }
    }
}