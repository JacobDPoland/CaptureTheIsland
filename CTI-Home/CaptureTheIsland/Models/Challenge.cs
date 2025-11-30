using System.ComponentModel.DataAnnotations;

namespace CaptureTheIsland.Models
{
    // POCO entity representing a persisted challenge (one table for all groups).
    public class Challenge
    {
        public int ChallengeId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Difficulty { get; set; } = string.Empty;

        [Required]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string Prompt { get; set; } = string.Empty;
        public string FileURL { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;

        // Serialized hints (JSON or newline-separated). If you need multiple rows, create a ChallengeHint entity.
        public string Hints { get; set; } = string.Empty;
    }
}
