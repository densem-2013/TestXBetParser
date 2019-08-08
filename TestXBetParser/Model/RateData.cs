using System;

namespace TestXBetParser.Model
{
    public class RateData
    {
        public string FirstTeamWinRate { get; set; }
        public string SecondTeamWinRate { get; set; }
        public string DrawRate { get; set; }
        public void Print()
        {
            Console.WriteLine($"FirstTeamWinRate = {FirstTeamWinRate}");

            Console.WriteLine($"SecondTeamWinRate = {SecondTeamWinRate}");

            Console.WriteLine($"DrawRate =  {DrawRate}");

            Console.WriteLine(new string('*', 20));
        }
    }
}
