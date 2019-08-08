using System;
using System.Collections.Generic;

namespace TestXBetParser.Model
{
    public class Liga
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Match> Matches { get; set; }

        public void PrintData()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
            Console.WriteLine($"Liga Title = {Title}");
            Console.WriteLine();

            foreach (var item in Matches)
            {
                item.Print();
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}
