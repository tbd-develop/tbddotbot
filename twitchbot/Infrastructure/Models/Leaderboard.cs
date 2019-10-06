using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace twitchbot.Infrastructure.Models
{
    public class Leaderboard
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string,int> Results { get; set; }
    }
}