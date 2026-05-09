using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        public required string Name { get; set; }

        public int LeagueId { get; set; }

        [JsonIgnore]
        public League? League { get; set; }

        public List<Player>? Palyers { get; set; }
    }
}
