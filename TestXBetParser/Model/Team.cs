using System;

namespace TestXBetParser.Model
{
    public class Team
    {
        public Team()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string TeamName { get; set; }
    }
}
