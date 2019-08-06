using System;

namespace TestXBetParser.Model
{
    public class Team
    {
        public Team(string teamName)
        {
            Id = Guid.NewGuid();
            TeamName = teamName;
        }
        public Guid Id { get; set; }
        public string TeamName { get; set; }
    }
}
