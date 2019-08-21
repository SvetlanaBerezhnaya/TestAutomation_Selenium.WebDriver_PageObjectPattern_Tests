using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectPatternTests.PageObjects;

namespace PageObjectPatternTests
{
    [TestFixture]
    public class TestClass
    {
        private IWebDriver driver;
        private static List<string> topStories_expectedResults = new List<string>();
        private static List<string> topStories_actualResults = new List<string>();
        private static Dictionary<string, string> values = new Dictionary<string, string>(5);

        [SetUp]
        public void SetUp()
        {
            this.driver = new ChromeDriver();
        }

        [TearDown]
        public void Close()
        {
            //driver.Close();
        }
        public void Init_topStories_results(List<string> topStories_results, bool topStoryFirst)
        {
            if (topStories_results == topStories_expectedResults)
            {
                topStories_expectedResults.Clear();
                if (topStoryFirst) topStories_expectedResults.Add("Hong Kong protests trample rule of law - China");
                else
                {
                    topStories_expectedResults.Add("Protests mark opening of European Parliament");
                    topStories_expectedResults.Add("Secret 'border patrol Facebook group' investigated");
                    topStories_expectedResults.Add("Deadly floods bring Mumbai to standstill");
                    topStories_expectedResults.Add("School shooting teacher's story revealed as hoax");
                    topStories_expectedResults.Add("Who is Gauff, the 15 - year - old who beat Venus Williams?");
                }
            }
            if (topStories_results == topStories_actualResults)
            {
                topStories_actualResults.Clear();
                NewsPage newsPage = new NewsPage(this.driver);
                if (topStoryFirst) topStories_actualResults.Add(newsPage.getText_TopStory_link(1));
                else
                    for (short i = 2; i < 7; i++)
                    {
                        topStories_actualResults.Add(newsPage.getText_TopStory_link(i));
                    }
            }
        }
        public void Init_dictionary_values(Dictionary<string, string> values, int amount)
        {
            string Lipsum;
            LoremIpsumPage loremIpsumPage = new LoremIpsumPage(this.driver);
            loremIpsumPage.GetLipsumText(amount, out Lipsum);
            string[] words = Lipsum.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            values.Clear();
            values.Add("What questions would you like us to investigate?", Lipsum);
            values.Add("Name", words[0] + " " + words[1]);
            values.Add("Email address", words[0] + "." + words[1] + "@gmail.com");
            values.Add("Age", "99");
            values.Add("Postcode", "999999");
            this.driver.Close();
        }
        public void TestingInputOfAdifferentNumberOfCharacters(int numberOfCharacters)
        {
            Init_dictionary_values(values, numberOfCharacters);
            this.driver = new ChromeDriver();
            HaveYourSayPage haveYourSayPage = new HaveYourSayPage(this.driver);
            haveYourSayPage.FillForm(values, false);
        }
        public void TestingOfInputEmptyElement(string emptyElement)
        {
            Init_dictionary_values(values, 140);
            values.Remove(emptyElement);
            this.driver = new ChromeDriver();
            HaveYourSayPage haveYourSayPage = new HaveYourSayPage(this.driver);
            haveYourSayPage.FillForm(values, true);

            string color = haveYourSayPage.getColor_label(emptyElement);
            Assert.That(color == "rgb(221, 0, 0)" || color == "rgba(221, 0, 0, 1)", "The text color of the '" + emptyElement + "' label is not red.");

            color = haveYourSayPage.getBorderColor_input(emptyElement);
            Assert.That(color == "rgb(221, 0, 0)" || color == "rgba(221, 0, 0, 1)", "The border color of the '" + emptyElement + "' input field is not red.");
        }

        [Test, Description("Task 1.1")]
        public void TestMethod1()
        {
            HomePage homePage = new HomePage(this.driver);
            homePage.goToPage();

            NewsPage newsPage = homePage.goToNewsPage();
            this.Init_topStories_results(topStories_expectedResults, true);
            this.Init_topStories_results(topStories_actualResults, true);
            Assert.Contains(topStories_expectedResults, topStories_actualResults);
        }

        [Test, Description("Task 1.2")]
        public void TestMethod2()
        {
            HomePage homePage = new HomePage(this.driver);
            homePage.goToPage();

            NewsPage newsPage = homePage.goToNewsPage();
            this.Init_topStories_results(topStories_expectedResults, false);
            this.Init_topStories_results(topStories_actualResults, false);
            foreach (string topStories_expectedResult in topStories_expectedResults)
            {
                Assert.Contains(topStories_expectedResult, topStories_actualResults);
            }
        }

        [Test, Description("Task 1.3")]
        public void TestMethod3()
        {
            HomePage homePage = new HomePage(this.driver);
            homePage.goToPage();

            NewsPage newsPage = homePage.goToNewsPage();
            string toSearch = newsPage.getText_TopStoryCategory_link(1);
            newsPage.Search(toSearch);
            SearchResultsPage searchResultsPage = newsPage.goToSearchResultsPage();
            string searchResultNumber1 = searchResultsPage.getText_SearchResultNumber1_link();
            Assert.AreEqual("One - minute World News", searchResultNumber1);
        }

        [Test, Description("Task 2.2")]
        public void TestMethod4()
        {
            TestingInputOfAdifferentNumberOfCharacters(140);
        }

        [Test, Description("Task 2.3")]
        public void TestMethod5()
        {
            TestingInputOfAdifferentNumberOfCharacters(141);
        }

        [Test, Description("Task 2.3")]
        public void TestMethod6()
        {
            TestingOfInputEmptyElement("Name");
        }

        [Test, Description("Task 2.3")]
        public void TestMethod7()
        {
            TestingOfInputEmptyElement("Email address");
        }
    }
}