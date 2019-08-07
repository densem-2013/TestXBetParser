using AngleSharp;
using AngleSharp.Html.Parser;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestXBetParser.Model;

namespace TestXBetParser.Services
{
    public class ParserService : IParserService
    {
        private IEnumerable<Liga> ParsedData { get; set; }
        private string _pageUrl { get; set; }
        public ParserService(string pageUrl)
        {
            _pageUrl = pageUrl;
        }
        public async Task ParseMatchesData()
        {
            ParsedData = new List<Liga>();
            var config = Configuration.Default.WithDefaultLoader().WithJs().WithCss();
            var parser = new HtmlParser();

            var httpClient = new HttpClient();
            var request = await httpClient.GetAsync(_pageUrl);
            var response = await request.Content.ReadAsStringAsync();

            //var document = parser.ParseDocument(response);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(_pageUrl);
            var dashChampContentSelector = "*[data-name|='dashboard-champ-content']";
            var cells = document.QuerySelectorAll(dashChampContentSelector);
            var ligas = cells.Select(m =>
            {
                var ligaDiv = m.QuerySelectorAll("div.c-events__name>a").FirstOrDefault();
                var ligaid = ligaDiv != null ? ligaDiv.GetAttribute("data-ligaid") : string.Empty;
                var ligaTitle = ligaDiv != null ? ligaDiv.GetAttribute("title") : string.Empty;

                return new Liga
                {
                    Id = ligaid,
                    Title = ligaTitle,
                    Matches = m.GetElementsByClassName("c-events__item,c-events__item_game,c-events-scoreboard__wrap").Select(c =>
                    {
                        var teamNamesSelector = "div.c-events-scoreboard div.c-events-scoreboard__item a.c-events__name div.c-events__team";
                        var teamNames = c.QuerySelectorAll(teamNamesSelector).Select(h => h.TextContent);
                        var matchUrlAttributeSelector = "a.c-events__name";
                        var url = c.QuerySelectorAll(matchUrlAttributeSelector).Select(h => h.Attributes["href"]?.Value).FirstOrDefault();
                        return new Match
                        {
                            FirstTeam = new Team(teamNames.ElementAtOrDefault(0)),
                            SecondTeam = new Team(teamNames.ElementAtOrDefault(1)),
                            Url = url

                        };
                    }).ToList()
                };
            });
        }
        public async Task SeleniumParse()
        {
            IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            driver.Navigate().GoToUrl(_pageUrl);
            //WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(15));
            //wait.Until(dr => dr.FindElements(By.CssSelector("div[data-name|='dashboard-champ-content']")));
            var elements = driver.FindElements(By.CssSelector("div[data-name|='dashboard-champ-content']"));
            for (int i = 0; i < elements.Count; i++)
            {
                IWebElement item = elements[i];
                var ligaItem = item.FindElement(By.CssSelector($"div>div.c-events__name>a"));

                var ligaid = ligaItem != null ? ligaItem.GetAttribute("data-ligaid") : string.Empty;
                var ligaTitle = ligaItem != null ? ligaItem.GetAttribute("title") : string.Empty;
            }
        }
        public async Task HtmlAgilityPackParse()
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(_pageUrl);
            var navigator = (HtmlNodeNavigator)htmlDoc.CreateNavigator();
            var nodes = navigator.Select("/div[data-name='dashboard-champ-content']");
            while (nodes.MoveNext())
            {
                var ligaItem = nodes.Current.SelectSingleNode("/div/div.c-events__name/a");

                var ligaid = ligaItem != null ? nodes.Current.GetAttribute("data-ligaid","") : string.Empty;
                var ligaTitle = ligaItem != null ? nodes.Current.GetAttribute("title","") : string.Empty;
            }
            //foreach (var node in nodes.)
            //{
            //    var ligaItem = node.SelectSingleNode("/div/div.c-events__name/a");

            //    var ligaid = ligaItem != null ? ligaItem.Attributes["data-ligaid"]?.Value : string.Empty;
            //    var ligaTitle = ligaItem != null ? ligaItem.Attributes["title"]?.Value : string.Empty;
            //}
        }
        public void PrintData()
        {
            Console.WriteLine(new string('*', 20));

            foreach (var item in ParsedData)
            {
                item.PrintData();
            }
        }
    }
}
