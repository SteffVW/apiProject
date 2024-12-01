﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIOpdracht_SteffVanWeereld.Models
{
    public class Boss
    {
        public int Id { get; set; }
        [Range(1, 138, ErrorMessage = "Combat level must be between 1 and 138.")]
        public int CombatLevel { get; set; }

        [Required]
        public int QuestId { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Max hit must be a positive number.")]
        public int MaxHit { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Examine text cannot exceed 500 characters.")]
        public string ExamineText { get; set; } = string.Empty;

        [JsonIgnore]
        public string Image { get; set; } = string.Empty;
    }
}
