using System;

namespace TestXBetParser.Model
{
    public class Match
    {
        public Match()
        {
            Score = new int[] { 0, 0 };
        }
        public int Id { get; set; }
        public string LigaId { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public string Url { get; set; }
        public int[] Score { get; set; }
        public string MatchTime { get; set; }
        public RateData RateData { get; set; }

        public void Print()
        {
            Console.WriteLine($"Match Id = {Id}");

            Console.WriteLine($"Match Url = {Url}");

            Console.WriteLine($"Teams: {FirstTeam.TeamName}-{SecondTeam.TeamName}");

            Console.WriteLine($"Match Score: {Score[0]}:{Score[1]}");

            Console.WriteLine($"Match Time = {MatchTime}");

            Console.WriteLine();
        }
        public void PrintRates()
        {
            Console.WriteLine($"RateData for match {FirstTeam.TeamName}-{SecondTeam.TeamName} : ");

            Console.WriteLine(new string('*', 20));

            RateData?.Print();
        }
    }
}
