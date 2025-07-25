using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballManager.Core.Entities;
public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string Position { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }

    public ICollection<Statistics> PlayerStats { get; set; }
}


