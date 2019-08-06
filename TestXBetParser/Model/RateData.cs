using System;

namespace TestXBetParser.Model
{
    public class RateData
    {
        public float FirstTeamWinRate { get; set; }
        public float SecondTeamWinRate { get; set; }
        public float DrawRate { get; set; }
        public void Print()
        {
            Console.WriteLine($"FirstTeamWinRate = {FirstTeamWinRate}");

            Console.WriteLine($"SecondTeamWinRate = {SecondTeamWinRate}");

            Console.WriteLine($"DrawRate =  {DrawRate}");

            Console.WriteLine(new string('*', 20));
        }
    }
}
