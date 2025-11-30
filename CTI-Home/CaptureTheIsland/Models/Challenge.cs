using Microsoft.AspNetCore.Mvc;

namespace CaptureTheIsland.Models
{
    // POCO entity representing a persisted challenge (one table for all groups).
    public class Challenge
    {
        public int ChallengeId { get; set; }
        public string Title { get; set; } = string.Empty;
        // Category (e.g. "Sample", "Password Cracking", "Cryptography", "OSINT", etc.)
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Prompt { get; set; } = string.Empty;
        public string FileURL { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;

        public List<string> Hints { get; set; } = new();
        

    }
}
