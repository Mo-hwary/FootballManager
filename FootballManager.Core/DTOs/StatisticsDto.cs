﻿using FootballManager.Core.Entities;

namespace FootballManager.Core.DTOs
{
    public class StatisticsDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string? PlayerName { get; set; }
        public int MatchId { get; set; }

        public int MinutesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }
} 
