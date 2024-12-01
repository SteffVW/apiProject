using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIOpdracht_SteffVanWeereld.Models
{
    public class Quest
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quest points must be a non-negative number.")]
        public int QuestPoints { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Length description cannot exceed 50 characters.")]
        public string Lenght { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Difficulty description cannot exceed 50 characters.")]
        public string Difficulty { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Series description cannot exceed 50 characters.")]
        public string Series { get; set; } = string.Empty;

        [Required(ErrorMessage = "BossId is required.")]
        public int BossId { get; set; }

        [Required(ErrorMessage = "RegionId is required.")]
        public int RegionId { get; set; }

        [JsonIgnore]
        public string Image { get; set; } = string.Empty;
    }
}
