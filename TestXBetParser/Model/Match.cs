using System;

namespace TestXBetParser.Model
{
    public class Match
    {
        public Match(Team firstTeam, Team secondTeam)
        {
            Id = Guid.NewGuid();
            FirstTeam = firstTeam;
            SecondTeam = secondTeam;
            Score = new int[] { 0, 0 };
        }
        public Guid Id { get; set; }
        public Team FirstTeam { get; }
        public Team SecondTeam { get; }
        public string Url { get; set; }
        public int[] Score { get; set; }
        public DateTime MatchTime { get; set; }
        public RateData RateData { get; set; }

        public void Print()
        {

            Console.WriteLine($"Match Id = {Id}");

            Console.WriteLine($"Match Url = {Url}");

            Console.WriteLine($"Teams: {FirstTeam.TeamName}-{SecondTeam.TeamName}");

            Console.WriteLine($"Match Score: {Score[0]}:{Score[1]}");

            Console.WriteLine($"Match Time = {MatchTime:dddd:MM:yy:HH:mm}");

            Console.WriteLine(new string('*', 20));
        }
        public void PrintRates()
        {
            Console.WriteLine(new string('*', 20));

            Console.WriteLine($"MatchId = {Id}");

            RateData.Print();
        }
    }
}
