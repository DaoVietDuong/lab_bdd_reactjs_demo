using LabReactjsDemo;
using LabReactjsDemo.Command;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowFeatures.Steps
{
    [Binding]
    public sealed class ItemStepDefinitions
    {
        
        static IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;
        static HttpClient _client = new HttpClient();
        static string _host = "http://localhost:52545";

        public ItemStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"ItemTable has values")]
        public async Task GivenItemTableHasValues(Table table)
        {
            await _client.GetAsync($"{_host}/api/databaseTest/clear");

            var commands = new List<CreateItemCommand>();

            foreach (var r in table.Rows)
            {
                commands.Add(new CreateItemCommand()
                {
                    Id = Guid.Parse(r["Id"]),
                    Name = r["Name"],
                    Description = r["Description"],
                    Quantity = int.Parse(r["Quantity"])
                });
            }
            var data = new StringContent(JsonConvert.SerializeObject(commands), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_host}/api/databaseTest/", data);

            response.EnsureSuccessStatusCode();
        }

        [Given(@"HomePage is opened")]
        public void GivenHomePageIsOpened()
        {
            driver = new FirefoxDriver($"{Environment.CurrentDirectory}/Drivers");
            driver.Url = "http://localhost:52545/";
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(2000);
        }

        [When(@"clicking Items on NavMenu")]
        public void WhenClickingItemsOnNavMenu()
        {
            var itemLink = driver.FindElement(By.XPath("/html/body/div[1]/div/header/nav/div/div/ul/li[3]/a"));
            itemLink.Click();
        }

        [Then(@"should see table like")]
        public void ThenShouldSeeTableLike(Table table)
        {
            var tbody = driver.FindElement(By.TagName("tbody"));
            var trs = tbody.FindElements(By.TagName("tr"));
            var items = new List<Item>();

            foreach (var tr in trs)
            {
                var td = tr.FindElements(By.TagName("td")).ToList();
                items.Add(new Item()
                {
                    Id = Guid.Parse(td[0].Text),
                    Name = td[1].Text,
                    Description = td[2].Text,
                    Quantity = int.Parse(td[3].Text)
                });
            }

            Assert.AreEqual(table.RowCount, items.Count);
            Assert.True(table.Rows[0]["Name"] == items[0].Name);
            Assert.True(table.Rows[1]["Name"] == items[1].Name);
            Assert.True(table.Rows[2]["Name"] == items[2].Name);
        }

        [Then(@"close browser")]
        public void ThenCloseBrowser()
        {
            driver.Quit();
        }


        [When(@"Enter Item like this")]
        public async Task WhenEnterItemLikeThis(Table table)
        {
            var name = driver.FindElement(By.XPath("//*[@id=\"name\"]"));
            name.SendKeys(table.Rows[0]["Name"]);

            var description = driver.FindElement(By.XPath("//*[@id=\"description\"]"));
            description.SendKeys(table.Rows[0]["Description"]);

            var quanlity = driver.FindElement(By.XPath("//*[@id=\"quantity\"]"));
            quanlity.SendKeys(table.Rows[0]["Quantity"]);
        }

        [When(@"Click Create button")]
        public void WhenClickCreateButton()
        {
            var createBtn = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/button"));
            createBtn.Click();
        }

        [Then(@"shoul see table like")]
        public void ThenShoulSeeTableLike(Table table)
        {
            var tbody = driver.FindElement(By.TagName("tbody"));
            var trs = tbody.FindElements(By.TagName("tr"));
            var items = new List<Item>();

            foreach (var tr in trs)
            {
                var td = tr.FindElements(By.TagName("td")).ToList();
                items.Add(new Item()
                {
                    Id = Guid.Parse(td[0].Text),
                    Name = td[1].Text,
                    Description = td[2].Text,
                    Quantity = int.Parse(td[3].Text)
                });
            }

            Assert.AreEqual(table.RowCount, items.Count);
            Assert.True(table.Rows[0]["Name"] == items[0].Name);
            Assert.True(table.Rows[1]["Name"] == items[1].Name);
            Assert.True(table.Rows[2]["Name"] == items[2].Name);
            Assert.True(table.Rows[3]["Name"] == items[3].Name);
        }


        [AfterTestRun]
        public static void AfterWebFeature()
        {
            driver.Dispose();
        }
    }
}
