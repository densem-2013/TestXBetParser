using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using TestXBetParser.Model;

namespace TestXBetParser.Services
{
    public class ParserService : IParserService
    {
        private List<Liga> ParsedData { get; set; }
        private string _pageUrl { get; set; }
        public ParserService(string pageUrl)
        {
            _pageUrl = pageUrl;
            ParsedData = new List<Liga>();
        }

        public void SeleniumParse()
        {
            IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            driver.Navigate().GoToUrl(_pageUrl);

            var elements = driver.FindElements(By.CssSelector("div[data-name|='dashboard-champ-content']"));

            var count = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                var liga = new Liga();
                IWebElement item = elements[i];
                var ligaItem = item.FindElement(By.CssSelector($"div>div.c-events__name>a"));

                liga.Id = ligaItem != null ? ligaItem.GetAttribute("data-ligaid") : string.Empty;
                liga.Title = ligaItem != null ? ligaItem.GetAttribute("title") : string.Empty;
                liga.Matches = item.FindElements(By.CssSelector($"div.c-events__item.c-events__item_col")).Select(c =>
                    {
                        var teamNamesSelector = "div.c-events-scoreboard div.c-events-scoreboard__item a.c-events__name div.c-events__team";
                        var teamNames = c.FindElements(By.CssSelector(teamNamesSelector)).Select(h => h.Text);
                        var matchUrlAttributeSelector = "a.c-events__name";
                        var url = c.FindElements(By.CssSelector(matchUrlAttributeSelector)).Select(h => h.GetAttribute("href")).FirstOrDefault();
                        var score = c.FindElements(By.CssSelector("span.c-events-scoreboard__cell.c-events-scoreboard__cell--all")).Select(h => int.Parse(h.Text)).ToArray();
                        var rates = c.FindElements(By.CssSelector("div.c-bets a")).Select(k => k.Text).Take(3).ToArray();
                        var time = c.FindElements(By.CssSelector("div.c-events__time span")).Select(k => k.Text).FirstOrDefault();
                        return new Match
                        {
                            Id = ++count,
                            FirstTeam = new Team { TeamName = teamNames.ElementAtOrDefault(0) },
                            SecondTeam = new Team { TeamName = teamNames.ElementAtOrDefault(1) },
                            Url = url,
                            LigaId = liga.Id,
                            Score = score,
                            RateData = rates.Length == 3 ? new RateData { FirstTeamWinRate = rates[0], SecondTeamWinRate = rates[2], DrawRate = rates[1] } : null,
                            MatchTime = time
                        };
                    }).ToList();

                ParsedData.Add(liga);
            }
        }

        public Match GetMatchById(int id)
        {
            return ParsedData.SelectMany(d => d.Matches).FirstOrDefault(m => m.Id == id);
        }
        public void PrintData()
        {
            foreach (var item in ParsedData)
            {
                item.PrintData();
            }
        }
    }
}
