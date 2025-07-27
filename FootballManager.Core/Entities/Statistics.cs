using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FootballManager.Core.Entities
{
    public class Statistics
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }

        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public int MinutesPlayed { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }

        public Player Player { get; set; }
        public Match Match { get; set; }

    }
}





