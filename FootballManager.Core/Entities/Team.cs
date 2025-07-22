using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace FootballManager.Core.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int FoundedYear { get; set; }
    public string Coach { get; set; }

    public ICollection<Player> Players { get; set; }
    public ICollection<Match> HomeMatches { get; set; }
    public ICollection<Match> AwayMatches { get; set; }
}


