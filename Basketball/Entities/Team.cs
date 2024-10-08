﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Basketball.Entities
{
    [Index("Name", IsUnique = true)]
    public class Team
    {
        public int Id { get; set; }

        [StringLength(16)]

        public string Name { get; set; }
        public string? Location { get; set; } 

        public Player BestPlayer => GetBestPlayer();
        public List<Player> Players { get; set; }

        public List<Game> Games { get; set; }
        public List<GameTeam> GameTeams { get; set; }


        public Player GetBestPlayer()
        {
            var bestPerecent = Players.Max(p => p.Statistics?.TotalPercent);
            var bestPlayer = Players.Where(p => p.Statistics?.TotalPercent ==  bestPerecent).FirstOrDefault();
            return bestPlayer;
        }
        

    }

   
}
