using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestXBetParser.Model;

namespace TestXBetParser.Services
{
    public class ParserService : IParserService
    {
        private IEnumerable<Match> ParsedData { get; set; }
        private string PageUrl { get; set; }
        public ParserService(string pageUrl)
        {
            PageUrl = pageUrl;
        }
        public async Task ParseMatchesData()
        {
           ParsedData = new List<Match>();
        }

        public void PrintData()
        {
            Console.WriteLine(new string('*', 20));

            foreach (var item in ParsedData)
            {
                item.Print();
            }
        }
    }
}
