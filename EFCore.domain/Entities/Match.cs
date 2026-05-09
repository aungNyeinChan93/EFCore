using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class Match
    {
        public int HomeTeamId { get; set; }

        [JsonIgnore]
        public Team HomeTeam { get; set; }


        public int AwayTeamId { get; set; }

        [JsonIgnore]
        public Team AwayTeam { get; set; }
    }
}
