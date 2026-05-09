using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class League
    {
        public int LeagueId { get; set; }

        public required string Name { get; set; }

        public List<Team>? Teams { get; set; }

        
    }
}
