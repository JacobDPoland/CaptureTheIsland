using System.Collections.Generic;

namespace CaptureTheIsland.Models
{
    public class ChallengeSummary
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public static IList<ChallengeSummary> SampleData() => new List<ChallengeSummary>
        {
            new() { Title = "Cipher Warm‑up", Type = "Cryptography", Difficulty = "Easy", Slug = "cipher-warmup" },
            new() { Title = "Apache Access Sleuth", Type = "Log Analysis", Difficulty = "Medium", Slug = "apache-access" },
            new() { Title = "Hash Stretch", Type = "Password Cracking", Difficulty = "Medium", Slug = "hash-stretch" },
            new() { Title = "Forensics: PNG Oddities", Type = "Forensics", Difficulty = "Hard", Slug = "png-oddities" }
        };
    }
}
