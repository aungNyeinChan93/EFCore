using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace EFCore.domain.Entities
{
    public class Player
    {
        [Key]
        public int PalyerId { get; set; }

        public required string Name { get; set; }

        public int Age { get; set; }

        public required string Country { get; set; }

        public int AssistScore { get; set; }

        public int GoalScore { get; set; }

        public int TeamId { get; set; }

        [JsonIgnore]
        public Team? Team { get; set; }
    }
}
