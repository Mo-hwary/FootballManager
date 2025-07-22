using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballManager.Core.Entities;
public class Match
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
    public string Stadium { get; set; }

    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }

    public ICollection<PlayerStat> PlayerStats { get; set; }
}



